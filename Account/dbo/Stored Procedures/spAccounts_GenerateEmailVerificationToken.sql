CREATE PROCEDURE [dbo].[spAccounts_GenerateEmailVerificationToken]
	@AccountID int,
	@UpdatedEmail varchar(100),
	@OutGUID varchar(100) out,
	@OutKey varchar(100) out
AS
	Select @OutGUID = CONVERT(VARCHAR(100), NEWID(), 2)
	Select @OutKey = CONVERT(VARCHAR(20), CRYPT_GEN_RANDOM(50), 2)

	IF Exists(Select * From Accounts_Core Where Email = @UpdatedEmail and AccountID != @AccountID)
	BEGIN
		RaisError('Email In Use', 16,1)
		return -1
	END
	ELSE IF Exists(Select * From Accounts_Core Where Email = @UpdatedEmail and AccountID = @AccountID and EmailVerified = 1)
	BEGIN
		return 0
	END
	ELSE
	BEGIN
		Insert Into Accounts_EmailVerificationToken(TokenID, TokenKey, AccountID, CreatedOn, UsedOn, Email) Values (@OutGUID, @OutKey, @AccountID, CURRENT_TIMESTAMP, null, @UpdatedEmail)
	END
RETURN 0