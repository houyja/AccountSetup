-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE spAccounts_EditAssignedRole
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
	Update Accounts_Role
	Set ExpirationDate = @ExpirationDate
	Where AccountID = @AccountID and RoleID = @RoleID
END