CREATE TABLE [dbo].[Accounts_Login] (
    [AccountID]      INT           NOT NULL,
    [HashedPassword] VARCHAR (100) NOT NULL,
    [Salt]           VARCHAR (100) NOT NULL,
    [LastEditedOn]   DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([AccountID] ASC),
    CONSTRAINT [FK_AccountsLogin_AccountsCore] FOREIGN KEY ([AccountID]) REFERENCES [dbo].[Accounts_Core] ([AccountID])
);

