CREATE TABLE [dbo].[Accounts_InvalidLogin] (
    [LoginAttemptID]   INT      NOT NULL,
    [AccountID]        INT      NOT NULL,
    [AttemptValidated] BIT      NOT NULL,
    [LoggedOn]         DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([LoginAttemptID] ASC),
    CONSTRAINT [FK_AccountsInvalidLogins_AccountsCore] FOREIGN KEY ([AccountID]) REFERENCES [dbo].[Accounts_Core] ([AccountID])
);

