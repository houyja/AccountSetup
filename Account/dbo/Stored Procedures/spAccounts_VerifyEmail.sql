CREATE PROCEDURE [dbo].[spAccounts_VerifyEmail]
	@TokenID varchar(100),
	@TokenKey varchar(100),
	@outAccountID int out
AS
	Select @outAccountID = AccountID From Accounts_EmailVerificationToken Where TokenID = @TokenID and TokenKey = @TokenKey and UsedOn is null
	IF(@outAccountID is not null)
	BEGIN
		IF Exists(Select * From Accounts_Core Where Email = (Select Email From Accounts_EmailVerificationToken where TokenID = @TokenID) and AccountID != @outAccountID)
		BEGIN
			RaisError('Email In Use',16,1)
			return -1
		END
		ELSE IF Exists(Select * From Accounts_Core Where Email = (Select Email From Accounts_EmailVerificationToken where TokenID = @TokenID) and AccountID = @outAccountID and EmailVerified = 1)
		BEGIN
			return 0
		END
		ELSE
		BEGIN
			Update Accounts_Core
			Set EmailVerified = 1, LastEditedOn = CURRENT_TIMESTAMP
			Where AccountID = @outAccountID

			Update Accounts_EmailVerificationToken
			Set UsedOn = CURRENT_TIMESTAMP
			Where AccountID = @outAccountID

			IF((Select Email From Accounts_EmailVerificationToken where TokenID = @TokenID) is not null)
			BEGIN
				Update Accounts_Core
				Set Email = (Select Email From Accounts_EmailVerificationToken where TokenID = @TokenID)
				Where AccountID = @outAccountID
			END
		END
	END
ELSE
	BEGIN
		RaisError('Invalid Token', 16, 1)
	END
RETURN 0