CREATE PROCEDURE [dbo].[spAccounts_UpdatePassword]
	@AccountID int,
	@CurPassword varchar(100),
	@NewPassword varchar(100),
	@NewSalt varchar(100)
AS
		IF EXISTS(Select * From Accounts_Login WHere AccountID = @AccountID and HashedPassword = @CurPassword)
		BEGIN
			Update Accounts_Login
			Set HashedPassword = @NewPassword, Salt = @NewSalt, LastEditedOn = CURRENT_TIMESTAMP
			Where AccountID = @AccountID
		END
		ELSE
		BEGIN
			RaisError('Incorrect Login Info', 16,1)
			Return -1
		END
RETURN 0