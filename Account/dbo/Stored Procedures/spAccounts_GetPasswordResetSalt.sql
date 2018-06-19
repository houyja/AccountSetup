CREATE PROCEDURE [dbo].[spAccounts_GetPasswordResetSalt]
	@GUID varchar(100),
	@outSalt varchar(100) out
AS
	IF Exists(Select TokenSalt From Accounts_PasswordResetToken where TokenID = @GUID)
	Begin
		Select @outSalt = TokenSalt From Accounts_PasswordResetToken where TokenID = @GUID
	END
	Else
	BEGIN
		RaisError('The Reset Token provided has either expired or is invalid. Please click the link below to generate a new reset token', 16, 1)
		return -1
	END
RETURN 0