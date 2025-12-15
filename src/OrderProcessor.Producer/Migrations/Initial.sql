IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [UnitOfMeasure] nvarchar(max) NOT NULL,
    [Quantity] int NOT NULL,
    [PricePerUnit] nvarchar(max) NOT NULL,
    [CreatedDateTime] datetime2 NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);

CREATE TABLE [TransportCompanies] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [ContactPhone] nvarchar(max) NOT NULL,
    [HeadquartersAddress_Line1] nvarchar(max) NOT NULL,
    [HeadquartersAddress_Line2] nvarchar(max) NULL,
    [HeadquartersAddress_City] nvarchar(max) NOT NULL,
    [HeadquartersAddress_StateOrProvince] nvarchar(max) NOT NULL,
    [HeadquartersAddress_PostalCode] nvarchar(max) NOT NULL,
    [HeadquartersAddress_Country] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_TransportCompanies] PRIMARY KEY ([Id])
);

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [CreatedUtc] datetime2 NOT NULL,
    [SenderAddress_Line1] nvarchar(max) NOT NULL,
    [SenderAddress_Line2] nvarchar(max) NULL,
    [SenderAddress_City] nvarchar(max) NOT NULL,
    [SenderAddress_StateOrProvince] nvarchar(max) NOT NULL,
    [SenderAddress_PostalCode] nvarchar(max) NOT NULL,
    [SenderAddress_Country] nvarchar(max) NOT NULL,
    [ReceiverAddress_Line1] nvarchar(max) NOT NULL,
    [ReceiverAddress_Line2] nvarchar(max) NULL,
    [ReceiverAddress_City] nvarchar(max) NOT NULL,
    [ReceiverAddress_StateOrProvince] nvarchar(max) NOT NULL,
    [ReceiverAddress_PostalCode] nvarchar(max) NOT NULL,
    [ReceiverAddress_Country] nvarchar(max) NOT NULL,
    [TransportCompanyId] int NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_TransportCompanies_TransportCompanyId] FOREIGN KEY ([TransportCompanyId]) REFERENCES [TransportCompanies] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [OrderProduct] (
    [OrderId] int NOT NULL,
    [ProductsId] int NOT NULL,
    CONSTRAINT [PK_OrderProduct] PRIMARY KEY ([OrderId], [ProductsId]),
    CONSTRAINT [FK_OrderProduct_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderProduct_Products_ProductsId] FOREIGN KEY ([ProductsId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_OrderProduct_ProductsId] ON [OrderProduct] ([ProductsId]);

CREATE INDEX [IX_Orders_TransportCompanyId] ON [Orders] ([TransportCompanyId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251213184753_Initial', N'10.0.1');

COMMIT;
GO

