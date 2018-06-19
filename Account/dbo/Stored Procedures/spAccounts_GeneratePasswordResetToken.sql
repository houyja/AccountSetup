CREATE PROCEDURE [dbo].[spAccounts_GeneratePasswordResetToken]
	@LoginID varchar(100),
	@OutGUID varchar(100) out,
	@outAccountID int out,
	@outEmail varchar(100) out,
	@Key varchar(100),
	@Salt varchar(100)
AS
	IF Exists(Select AccountID From Accounts_Core where Email = @LoginID or Username = @LoginID)
	BEGin
		Select @outAccountID = AccountID, @outEmail = Email From Accounts_Core where Email = @LoginID or Username = @LoginID

		Declare @ExpirationDate as datetime

		Select @OutGUID = CONVERT(VARCHAR(100), NEWID(), 2)

		IF((Select ConfigValue from Systems_Config where Config = 'PasswordReset_ExpirationTime (Hours)') > 0)
		BEGIN
			Select @ExpirationDate = DATEADD(HOUR, (Select ConfigValue from Systems_Config where Config = 'PasswordReset_ExpirationTime (Hours)'), CURRENT_TIMESTAMP)
		END
		ELSE
		BEGIN
			Select @ExpirationDate = DATEADD(HOUR, (3), CURRENT_TIMESTAMP)
		END
		
		Insert Into Accounts_PasswordResetToken (TokenID, TokenValidationKey, TokenSalt, AccountID, ExpirationDate, Valid, CreatedOn, LastEditedOn) Values (@OutGUID, @Key, @Salt, @outAccountID, @ExpirationDate, 1, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
			
		Update Accounts_PasswordResetToken
		Set Valid = 0
		Where AccountID = @outAccountID and Valid = 1 and TokenID != @OutGUID
	END
	ELSE
	BEGIN
		RaisError('The Account with the Provided Information could not be found', 16, 1)
		return -1
	END
RETURN 0