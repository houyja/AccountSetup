-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spRoles_GetUserPermissions]
	-- Add the parameters for the stored procedure here
	@AccountID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select Accounts_Role.RoleID, Accounts_Role.ExpirationDate, Roles_Role.RoleName, Roles_Permission.PermissionID, Roles_Permission.PermissionName, Roles_Permission.Controller, Roles_Permission.Action, Roles_Permission.Priority as PermissionPriority, Roles_PermissionGroup.PermissionGroupID, Roles_PermissionGroup.PermissionGroupName, Roles_PermissionGroup.Priority as PermissionGroupPriority From Accounts_Role
	Join Roles_Role on Roles_Role.RoleID = Accounts_Role.RoleID
	Join Roles_RolePermission on Roles_RolePermission.RoleID = Accounts_Role.RoleID
	Join Roles_Permission on Roles_Permission.PermissionID = Roles_RolePermission.PermissionID
	Join Roles_PermissionGroup on Roles_PermissionGroup.PermissionGroupID = Roles_Permission.PermissionGroupID
	Where Accounts_Role.AccountID = @AccountID
END