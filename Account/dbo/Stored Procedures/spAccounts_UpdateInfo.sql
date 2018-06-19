CREATE PROCEDURE [dbo].[spAccounts_UpdateInfo]
	@AccountID int,
	@FirstName varchar(100),
	@LastName varchar(100)
AS
		IF EXISTS(Select * From Accounts_Core Where AccountID = @AccountID)
		BEGIN
			Update Accounts_Core
			Set FirstName = @FirstName, LastName = @LastName, LastEditedOn = CURRENT_TIMESTAMP
			Where AccountID = @AccountID
		END
		ELse
		BEGIN
			RaisError('Invalid Account', 16, 1)
			return -1;
		END
RETURN 0