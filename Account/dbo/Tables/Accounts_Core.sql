CREATE TABLE [dbo].[Accounts_Core] (
    [AccountID]     INT           NOT NULL,
    [Username]      VARCHAR (100) NOT NULL,
    [Email]         VARCHAR (100) NOT NULL,
    [EmailVerified] BIT           NOT NULL,
    [FirstName]     VARCHAR (100) NULL,
    [LastName]      VARCHAR (100) NULL,
    [CreatedOn]     DATETIME      NOT NULL,
    [LastEditedOn]  DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([AccountID] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC)
);

