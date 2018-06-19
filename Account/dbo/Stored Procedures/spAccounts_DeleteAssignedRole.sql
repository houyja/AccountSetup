-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE spAccounts_DeleteAssignedRole
	-- Add the parameters for the stored procedure here
	@AccountID int,
	@RoleID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete From Accounts_Role Where AccountID = @AccountID and RoleID = @RoleID
END