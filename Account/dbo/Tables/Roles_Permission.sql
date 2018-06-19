CREATE TABLE [dbo].[Roles_Permission] (
    [PermissionID]      INT           NOT NULL,
    [PermissionName]    VARCHAR (100) NULL,
    [Action]            VARCHAR (100) NOT NULL,
    [Controller]        VARCHAR (100) NOT NULL,
    [PermissionGroupID] INT           NOT NULL,
    [Priority]          INT           NULL,
    PRIMARY KEY CLUSTERED ([PermissionID] ASC),
    CONSTRAINT [FK_Permissions_PermissionGroups] FOREIGN KEY ([PermissionGroupID]) REFERENCES [dbo].[Roles_PermissionGroup] ([PermissionGroupID])
);

