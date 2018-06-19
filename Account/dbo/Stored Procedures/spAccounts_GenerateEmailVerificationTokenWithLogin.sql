CREATE PROCEDURE [dbo].[spAccounts_GenerateEmailVerificationTokenWithLogin]
	@LoginID varchar(100),
	@OutGUID varchar(100) out,
	@OutKey varchar(100) out,
	@outEmail varchar(100) out
	
	AS
	Select @OutGUID = CONVERT(VARCHAR(100), NEWID(), 2)
	Select @OutKey = CONVERT(VARCHAR(20), CRYPT_GEN_RANDOM(50), 2)

	Declare @AccountID int


	IF Exists(Select AccountID From Accounts_Core Where Email = @LoginID or Username = @LoginID)
	BEGIN
		Select @AccountID = AccountID, @outEmail = Email From Accounts_Core Where Email = @LoginID or Username = @LoginID
		Insert Into Accounts_EmailVerificationToken(TokenID, TokenKey, AccountID, CreatedOn, UsedOn, Email) Values (@OutGUID, @OutKey, @AccountID, CURRENT_TIMESTAMP, null, null)
	END
	ELSE
	BEGIN
		RaisError('Login not Found', 16,1)
		return -1
	END
RETURN 0