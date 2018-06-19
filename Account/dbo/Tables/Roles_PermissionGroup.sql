CREATE TABLE [dbo].[Roles_PermissionGroup] (
    [PermissionGroupID]   INT           NOT NULL,
    [Priority]            INT           NOT NULL,
    [PermissionGroupName] VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([PermissionGroupID] ASC)
);

