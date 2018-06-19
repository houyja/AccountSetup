CREATE TABLE [dbo].[Accounts_PasswordResetToken] (
    [TokenID]            VARCHAR (100) NOT NULL,
    [TokenValidationKey] VARCHAR (100) NOT NULL,
    [AccountID]          INT           NOT NULL,
    [Valid]              BIT           NOT NULL,
    [ExpirationDate]     DATETIME      NOT NULL,
    [CreatedOn]          DATETIME      NOT NULL,
    [LastEditedOn]       DATETIME      NOT NULL,
    [TokenSalt]          VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([TokenID] ASC),
    CONSTRAINT [FK_AccountsResetToken_Accounts] FOREIGN KEY ([AccountID]) REFERENCES [dbo].[Accounts_Core] ([AccountID])
);

