-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE spRoles_EditPermissionGroup
	-- Add the parameters for the stored procedure here
	@PermissionGroupID int,
	@PermissionGroupName varchar(100),
	@PermissionGroupPriority int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Roles_PermissionGroup
	Set PermissionGroupName = @PermissionGroupName,
	Roles_PermissionGroup.Priority = @PermissionGroupPriority
	Where PermissionGroupID = @PermissionGroupID
END