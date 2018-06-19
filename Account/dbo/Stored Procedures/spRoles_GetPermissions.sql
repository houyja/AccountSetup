-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spRoles_GetPermissions]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select Roles_Permission.PermissionID, Roles_Permission.PermissionName, Roles_Permission.Controller, Roles_Permission.Action, Roles_Permission.Priority as PermissionPriority, Roles_PermissionGroup.PermissionGroupID, Roles_PermissionGroup.PermissionGroupName, Roles_PermissionGroup.Priority as PermissionGroupPriority From Roles_Permission
	Join Roles_PermissionGroup on Roles_PermissionGroup.PermissionGroupID = Roles_Permission.PermissionGroupID
END