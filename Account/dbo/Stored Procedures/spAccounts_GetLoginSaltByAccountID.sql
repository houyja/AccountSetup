CREATE PROCEDURE [dbo].[spAccounts_GetLoginSaltByAccountID]
	@AccountID int,
	@outSalt varchar(100) out
AS
	IF Exists(Select AccountID From Accounts_Core Where AccountID = @AccountID)
	Begin
		Select @outSalt = Salt From Accounts_Login
		Join Accounts_Core on Accounts_Core.AccountID = Accounts_Login.AccountID
		Where Accounts_Core.AccountID = @AccountID
	END
	Else
	BEGIN
		RaisError('Account Not Found', 16,1)
		return -1
	END
RETURN 0