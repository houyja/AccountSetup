-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE spRoles_DeletePermissionGroup
	-- Add the parameters for the stored procedure here
	@PermissionGroupID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete From Roles_RolePermission
	Where PermissionID in (Select PermissionID From Roles_Permission Where PermissionGroupID = @PermissionGroupID)
	
	Delete From Roles_Permission
	Where PermissionGroupID = @PermissionGroupID
	
	Delete From Roles_PermissionGroup
	WHere PermissionGroupID = @PermissionGroupID
END
