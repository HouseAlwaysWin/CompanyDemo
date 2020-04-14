
CREATE TABLE CompanyT_ProductT(
	CompanyID int NOT NULL,
	ProductID int NOT NULL ,
	CONSTRAINT [PK_dbo.CompanyT_ProductT] PRIMARY KEY ([CompanyID],[ProductID])
)

GO
CREATE INDEX [IX_CompanyID] ON [dbo].[CompanyT_ProductT]([CompanyID])
GO
CREATE INDEX [IX_ProductID] ON [dbo].[CompanyT_ProductT]([ProductID])
GO
ALTER TABLE [dbo].[CompanyT_ProductT] ADD CONSTRAINT [FK_dbo.CompanyT_ProductT_dbo.CompanyT_CompanyID] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[CompanyT] ([CompanyID]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CompanyT_ProductT] ADD CONSTRAINT [FK_dbo.CompanyT_ProductT_dbo.ProductT_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[ProductT] ([ProductID]) ON DELETE CASCADE
