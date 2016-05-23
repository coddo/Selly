﻿CREATE TABLE [dbo].[Orders] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [ClientId]   UNIQUEIDENTIFIER NOT NULL,
    [SaleTypeId] UNIQUEIDENTIFIER NOT NULL,
    [CurrencyId] UNIQUEIDENTIFIER NOT NULL,
    [Date]       DATETIME         NOT NULL,
    [Status]     INT              NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Orders_Clients] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id]),
    CONSTRAINT [FK_Orders_Currencies] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currencys] ([Id]),
    CONSTRAINT [FK_Orders_SaleTypes] FOREIGN KEY ([SaleTypeId]) REFERENCES [dbo].[SaleTypes] ([Id])
);

