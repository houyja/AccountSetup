﻿-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spRoles_DeletePermission]
	-- Add the parameters for the stored procedure here
	@PermissionID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete From Roles_RolePermission
	Where PermissionID = @PermissionID
	
	Delete From Roles_Permission
	Where PermissionID = @PermissionID
END
