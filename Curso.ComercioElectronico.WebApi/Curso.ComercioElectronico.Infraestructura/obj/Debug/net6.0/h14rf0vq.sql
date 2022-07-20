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

