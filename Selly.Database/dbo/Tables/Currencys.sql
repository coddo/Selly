CREATE TABLE [dbo].[Currencys] (
    [Id]    UNIQUEIDENTIFIER NOT NULL,
    [Name]  NVARCHAR (10)    NOT NULL,
    [Multiplier] FLOAT (53)       NOT NULL,
    CONSTRAINT [PK_Currencys] PRIMARY KEY CLUSTERED ([Id] ASC)
);

