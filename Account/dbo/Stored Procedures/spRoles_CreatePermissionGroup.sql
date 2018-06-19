-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spRoles_CreatePermissionGroup]
	-- Add the parameters for the stored procedure here
	@PermissionGroupName varchar(100),
	@PermissionPriority int,
	@PermissionGroupID int out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select @PermissionGroupID = Max(PermissionGroupID) + 1 from Roles_PermissionGroup
	IF(@PermissionGroupID is null)
	BEGIN
		Select @PermissionGroupID = 1
	END
	
	IF Exists(Select * From Roles_PermissionGroup Where PermissionGroupName = @PermissionGroupName)
	BEGIN
		RAISERROR('Group Name in use', 16, 1)
		return -1
	END

	Insert Into Roles_PermissionGroup(PermissionGroupID, PermissionGroupName, Priority) Values(@PermissionGroupID, @PermissionGroupName, @PermissionPriority)
END