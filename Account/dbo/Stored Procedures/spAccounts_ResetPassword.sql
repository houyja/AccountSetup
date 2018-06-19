CREATE PROCEDURE [dbo].[spAccounts_ResetPassword]
	@TokenID varchar(100),
	@TokenKey varchar(100),
	@Password varchar(100),
	@Salt varchar(100),
	@outAccountID int out
AS
	Exec spAccounts_VerifyPasswordResetToken @TokenID, @TokenKey, @outAccountID out

	IF(@outAccountID is not null)
	BEGIn
		Update Accounts_Login
		Set HashedPassword = @Password, Salt = @Salt, LastEditedOn = CURRENT_TIMESTAMP
		Where AccountID = @outAccountID

		Update Accounts_PasswordResetToken
		Set Valid = 0 
		Where AccountID = @outAccountID

		Update Accounts_InvalidLogin
		Set AttemptValidated = 1
		Where AccountID = @outAccountID
	END
RETURN 0