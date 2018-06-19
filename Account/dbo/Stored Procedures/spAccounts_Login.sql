CREATE PROCEDURE [dbo].[spAccounts_Login]
	@LoginID varchar(100),
	@Hash varchar(max),
	@outAccountID int out,
	@outUsername varchar(100) out,
	@outEmail varchar(100) out,
	@outFirstName varchar(100) out,
	@outLastName varchar(100) out,
	@outCretedOn datetime out,
	@outLastEditedOn datetime out

AS
	IF Exists(Select AccountID From Accounts_Core where Email = @LoginID or Username = @LoginID)
	BEGin
		Select @outAccountID = AccountID From Accounts_Core where Email = @LoginID or Username = @LoginID

		IF Exists(Select * From Accounts_Login Where AccountID = @outAccountID and HashedPassword = @Hash)
		Begin
			If((Select Count(*) From Accounts_InvalidLogin where AccountID = @outAccountID and AttemptValidated = 0) >= (Select ConfigValue from Systems_Config where Config = 'InvalidLogins_ValidAttempts') and (Select ConfigValue from Systems_Config where Config = 'InvalidLogins_ValidAttempts') > 0)
			BEGIN
				RaisError('Your Account has been locked due to repeated failed login attempts. Please reset your password to regain access to your account', 16, 1)
				Return -1
			END
			Else If((Select EmailVerified from Accounts_Core where AccountID = @outAccountID) = 0)
			BEGIN
				RaisError('Your Email is currently unverified. Please verify your email address before logging in. If you need to resend the verification email please click the link below', 16, 1)
				Return -1
			END
			Else
			BEGIN
				Select @outAccountID = AccountID, @outUsername = Username, @outEmail = Email, @outFirstName = FirstName, @outLastName = LastName, @outCretedOn = CreatedOn, @outLastEditedOn = LastEditedOn From Accounts_Core where Email = @LoginID or Username = @LoginID
				Update Accounts_InvalidLogin
				Set AttemptValidated = 1
				Where AccountID = @outAccountID
			END
		End
		Else
		BEGIN
			Insert Into Accounts_InvalidLogin(LoginAttemptID,AccountID, AttemptValidated, LoggedOn) Values ((Select Count(*) + 1 From Accounts_InvalidLogin), @outAccountID, 0, CURRENT_TIMESTAMP)

			If((Select Count(*) From Accounts_InvalidLogin where AccountID = @outAccountID and AttemptValidated = 0) >= (Select ConfigValue from Systems_Config where Config = 'InvalidLogins_ValidAttempts') and (Select ConfigValue from Systems_Config where Config = 'InvalidLogins_ValidAttempts') > 0)
			BEGIN
				RaisError('Your Account has been locked due to repeated failed login attempts. Please reset your password to regain access to your account', 16, 1)
				Return -1
			END
			RaisError('Invalid Username or Password', 16, 1)
			Return -1
		END
	END
	Else
	BEGIN
		RaisError('Invalid Username or Password', 16, 1)
		Return -1
	END
RETURN 0