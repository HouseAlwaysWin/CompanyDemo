
CREATE TABLE ProductT(
	ProductID int NOT NULL IDENTITY,
	ProductName varchar(100) NOT NULL,
	ProductType varchar(50) NOT NULL,
	Price INT NOT NULL,
	Unit varchar(10) NULL,
	CreatedDate datetime NOT NULL DEFAULT GETUTCDATE(),
	EditedDate datetime NOT NULL DEFAULT GETUTCDATE(),
	CONSTRAINT [PK_dbo.ProductT] PRIMARY KEY ([ProductID])
)
GO
CREATE UNIQUE INDEX [ProductIDIndex] ON [dbo].[ProductT]([ProductID])