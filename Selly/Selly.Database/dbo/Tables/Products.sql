CREATE TABLE [dbo].[Products] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [VatId]    UNIQUEIDENTIFIER NOT NULL,
    [Name]     NVARCHAR (MAX)   NOT NULL,
    [Quantity] FLOAT (53)       CONSTRAINT [DF_Products_Quantity] DEFAULT ((0)) NOT NULL,
    [Price]    FLOAT (53)       CONSTRAINT [DF_Products_Price] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Products_Vats] FOREIGN KEY ([VatId]) REFERENCES [dbo].[ValueAddedTaxes] ([Id])
);

