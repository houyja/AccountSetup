-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE spRoles_AddPermissionToRole
	-- Add the parameters for the stored procedure here
	@RoleID int,
	@PermissionID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert Into Roles_RolePermission(RoleID, PermissionID) Values (@RoleID, @PermissionID)
END