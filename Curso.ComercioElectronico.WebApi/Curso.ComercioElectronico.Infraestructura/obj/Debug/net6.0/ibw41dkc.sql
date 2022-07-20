BEGIN TRANSACTION;
GO

DROP TABLE [Clientes];
GO

DROP TABLE [Productos];
GO

CREATE TABLE [Products] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220627175103_AddProductEntity', N'6.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ProductTypes] (
    [Code] nvarchar(4) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ProductTypes] PRIMARY KEY ([Code])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220627185033_AddProductType', N'6.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Brands] (
    [Code] nvarchar(4) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Brands] PRIMARY KEY ([Code])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220628131636_AddBrand', N'6.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductTypes]') AND [c].[name] = N'Description');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ProductTypes] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ProductTypes] ALTER COLUMN [Description] nvarchar(256) NOT NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Name');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Products] ALTER COLUMN [Name] nvarchar(100) NOT NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Brands]') AND [c].[name] = N'Description');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Brands] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Brands] ALTER COLUMN [Description] nvarchar(256) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220628141712_AddConfiguration', N'6.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Products] ADD [BrandId] nvarchar(4) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Products] ADD [Description] nvarchar(256) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Products] ADD [Price] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [Products] ADD [ProductTypeId] nvarchar(4) NOT NULL DEFAULT N'';
GO

CREATE INDEX [IX_Products_BrandId] ON [Products] ([BrandId]);
GO

CREATE INDEX [IX_Products_ProductTypeId] ON [Products] ([ProductTypeId]);
GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_Brands_BrandId] FOREIGN KEY ([BrandId]) REFERENCES [Brands] ([Code]) ON DELETE NO ACTION;
GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_ProductTypes_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [ProductTypes] ([Code]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220628154214_AddConfigurationUpdate', N'6.0.6');
GO

COMMIT;
GO

