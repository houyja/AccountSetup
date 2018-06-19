-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE spRoles_EditRole
	-- Add the parameters for the stored procedure here
	@RoleID int,
	@RoleName varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF Exists(Select * From Roles_Role where RoleName = @RoleName)
	BEGIN
		RaisError('Name in Use', 16, 1)
		return -1
	END

	Update Roles_Role
	Set RoleName = @RoleName
	Where RoleID = @RoleID
END