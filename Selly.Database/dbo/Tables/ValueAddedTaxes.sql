CREATE TABLE [dbo].[ValueAddedTaxes] (
    [Id]    UNIQUEIDENTIFIER NOT NULL,
    [Value] FLOAT (53)       NOT NULL,
    CONSTRAINT [PK_ValueAddedTaxes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

