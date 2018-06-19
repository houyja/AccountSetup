-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE spRoles_EditPermission
	-- Add the parameters for the stored procedure here
	@PermissionID int,
	@PermissionName varchar(100),
	@Action varchar(100),
	@Controller varchar(100),
	@PermissionGroupID int,
	@Priority int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Roles_Permission
	Set PermissionName = @PermissionName,
	PermissionGroupID = @PermissionGroupID,
	Controller = @Controller,
	Action = @Action,
	Priority = @Priority
	Where PermissionID = @PermissionID
END