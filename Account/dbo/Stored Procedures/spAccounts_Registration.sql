CREATE PROCEDURE [dbo].[spAccounts_Registration]
	@Username varchar(100),
	@Email varchar(100),
	@FirstName varchar(100),
	@LastName varchar(100),
	@Hash varchar(100),
	@Salt varchar(100),
	@AccountID int out,
	@EmailVerificationToken varchar(100) out,
	@EmailVerificationKey varchar(100) out
AS
	IF Exists(Select * From Accounts_Core Where Username = @Username and Email = @Email)
	BEGIN
		RAISERROR('Username and Email are both in use', 16, 1)
		return -1
	END
	IF Exists(Select * From Accounts_Core Where Username = @Username)
	BEGIN
		RAISERROR('Username in use', 16, 1)
		return -1
	END
	IF Exists(Select * From Accounts_Core Where Email = @Email)
	BEGIN
		RAISERROR('Email in use', 16, 1)
		return -1
	END

	Declare @EmailVerified bit,
			@CreatedOn datetime,
			@LastEditedOn datetime

	Select @EmailVerified = 0
	Select @CreatedOn = CURRENT_TIMESTAMP
	Select @LastEditedOn = CURRENT_TIMESTAMP

	Select @AccountID = MAX(AccountID) + 1 From Accounts_Core

	IF(@AccountID is null)
	BEGIN
		Select @AccountID = 1
	END

	IF(@AccountID is not null)
	BEGIN
		Insert Into Accounts_Core (AccountID, Username, Email, EmailVerified, FirstName, LastName, CreatedOn, LastEditedOn) Values (@AccountID, @Username, @Email, @EmailVerified,@FirstName, @LastName, @CreatedOn, @LastEditedOn)
		Insert Into Accounts_Login (AccountID, HashedPassword, Salt, LastEditedOn) Values (@AccountID, @Hash, @Salt, @LastEditedOn)
		Exec spAccounts_GenerateEmailVerificationToken @AccountID, null, @EmailVerificationToken out, @EmailVerificationKey out
	END
	ELSE
	BEGIN
		RAISERROR('An Unexpected Error Occurred', 16, 1)
		return -1
	END
RETURN 0