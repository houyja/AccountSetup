CREATE PROCEDURE [dbo].[spAccounts_GetLoginSalt]
	@LoginID varchar(100),
	@outSalt varchar(100) out
AS
	IF Exists(Select AccountID From Accounts_Core Where Username = @LoginID or Email = @LoginID)
	Begin
		Select @outSalt = Salt From Accounts_Login
		Join Accounts_Core on Accounts_Core.AccountID = Accounts_Login.AccountID
		Where Accounts_Core.Username = @LoginID or Email = @LoginID
	END
	Else
	BEGIN
		RaisError('Invalid Username or Password', 16,1)
		return -1
	END
RETURN 0