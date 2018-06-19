CREATE PROCEDURE [dbo].[spAccounts_VerifyPasswordResetToken]
	@TokenID varchar(100),
	@TokenKey varchar(100),
	@outAccountID int out
AS
	IF Exists(Select * From Accounts_PasswordResetToken Where TokenID = @TokenID and TokenValidationKey = @TokenKey and Valid = 1 and (ExpirationDate > CURRENT_TIMESTAMP or ExpirationDate is null))
	BEGIN
		Select @outAccountID = AccountID From Accounts_PasswordResetToken Where TokenID = @TokenID and TokenValidationKey = @TokenKey and Valid = 1 and (ExpirationDate > CURRENT_TIMESTAMP or ExpirationDate is null)
	END
	Else IF Exists(Select * From Accounts_PasswordResetToken Where TokenID = @TokenID and TokenValidationKey = @TokenKey)
	BEGIN
		RaisError('Invalid Token', 16,1)
		return -1
	END
	Else
	BEGIN
		RaisError('Invalid Token', 16,1)
		return -1
	END
RETURN 0