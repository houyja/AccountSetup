-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE spAccounts_AssignRole
	-- Add the parameters for the stored procedure here
	@AccountID int,
	@RoleID int,
	@ExpirationDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert Into Accounts_Role (AccountID, RoleID, ExpirationDate) Values(@AccountID, @RoleID, @ExpirationDate) 
END