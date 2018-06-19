-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE spRoles_DeleteRole
	-- Add the parameters for the stored procedure here
	@RoleID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete From Accounts_Role Where RoleID = @RoleID
	Delete From Roles_RolePermission Where RoleID = @RoleID
	Delete From Roles_Role Where RoleID = @RoleID
END
