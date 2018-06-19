CREATE TABLE [dbo].[Systems_ExceptionLog] (
    [ExceptionID]      INT           NOT NULL,
    [ExceptionSource]  VARCHAR (MAX) NULL,
    [AccountID]        INT           NULL,
    [LoggedTime]       DATETIME      NULL,
    [ExceptionError]   VARCHAR (MAX) NULL,
    [ReproductionData] VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ExceptionID] ASC)
);

