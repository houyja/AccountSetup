CREATE TABLE [dbo].[Accounts_Role] (
    [AccountID]      INT      NOT NULL,
    [RoleID]         INT      NOT NULL,
    [ExpirationDate] DATETIME NULL,
    CONSTRAINT [PK_Accounts_Role] PRIMARY KEY CLUSTERED ([AccountID] ASC, [RoleID] ASC),
    CONSTRAINT [FK_Accounts_Role_Accounts_Core] FOREIGN KEY ([AccountID]) REFERENCES [dbo].[Accounts_Core] ([AccountID]),
    CONSTRAINT [FK_Accounts_Role_Roles_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Roles_Role] ([RoleID])
);

