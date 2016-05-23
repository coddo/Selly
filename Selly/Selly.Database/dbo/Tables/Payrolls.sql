CREATE TABLE [dbo].[Payrolls] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [ClientId] UNIQUEIDENTIFIER NOT NULL,
    [OrderId]  UNIQUEIDENTIFIER NOT NULL,
    [Date]     DATETIME         NOT NULL,
    [Value]    FLOAT (53)       NOT NULL,
    CONSTRAINT [PK_Payrolls] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Payrolls_Clients] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id]),
    CONSTRAINT [FK_Payrolls_Orders] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([Id])
);

