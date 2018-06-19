CREATE TABLE [dbo].[Accounts_EmailVerificationToken] (
    [TokenID]   VARCHAR (100) NOT NULL,
    [TokenKey]  VARCHAR (100) NOT NULL,
    [AccountID] INT           NOT NULL,
    [CreatedOn] DATETIME      NOT NULL,
    [UsedOn]    DATETIME      NULL,
    [Email]     VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([TokenID] ASC),
    CONSTRAINT [FK_Accounts_EmailVerificationToken_ToTable] FOREIGN KEY ([AccountID]) REFERENCES [dbo].[Accounts_Core] ([AccountID])
);

