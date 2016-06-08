CREATE TABLE [dbo].[LogEntries] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [TimeStamp] DATETIME2 (7)  NULL,
    [Level]     NVARCHAR (15)  NULL,
    [Logger]    NVARCHAR (128) NULL,
    [Exception] NVARCHAR (MAX) NULL,
    [Message]   NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

