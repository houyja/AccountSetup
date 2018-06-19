-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE spRoles_CreateRole
	-- Add the parameters for the stored procedure here
	@RoleName varchar(100),
	@RoleID int out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select @RoleID = MAX(RoleID) + 1 from Roles_Role
	if(@RoleID is null)
	BEGIN
		Select @RoleID = 1
	END
    -- Insert statements for procedure here
	IF Exists(Select * From Roles_Role where RoleName = @RoleName)
	BEGIN
		RaisError('Name in Use', 16, 1)
		return -1
	END

	Insert Into Roles_Role(RoleID, RoleName) Values (@RoleID, @RoleName)
END