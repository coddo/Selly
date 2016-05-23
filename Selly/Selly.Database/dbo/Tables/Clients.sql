CREATE TABLE [dbo].[Clients] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [FirstName] NVARCHAR (100)   NOT NULL,
    [LastName]  NVARCHAR (100)   NOT NULL,
    [Email]     NVARCHAR (300)   NOT NULL,
    CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED ([Id] ASC)
);

