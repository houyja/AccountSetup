-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spRoles_CreatePermission]
	-- Add the parameters for the stored procedure here
	@PermissionName varchar(100),
	@Action varchar(100),
	@Controller varchar(100),
	@PermissionGroupID int,
	@Priority int,
	@PermissionID int out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Select @PermissionID = Max(PermissionID) + 1 from Roles_Permission
	IF(@PermissionID is null)
	BEGIN
		Select @PermissionID = 1
	END

    -- Insert statements for procedure here
	Insert Into Roles_Permission(PermissionID, PermissionName, PermissionGroupID, Controller, Action, Priority) Values (@PermissionID, @PermissionName, @PermissionGroupID, @Controller, @Action, @Priority)
END