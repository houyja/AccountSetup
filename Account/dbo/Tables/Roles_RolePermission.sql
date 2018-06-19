CREATE TABLE [dbo].[Roles_RolePermission] (
    [RoleID]       INT NOT NULL,
    [PermissionID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([RoleID] ASC, [PermissionID] ASC),
    CONSTRAINT [FK_RolePermissions_Permissions] FOREIGN KEY ([PermissionID]) REFERENCES [dbo].[Roles_Permission] ([PermissionID]),
    CONSTRAINT [FK_RolePermissions_Roles] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Roles_Role] ([RoleID])
);

