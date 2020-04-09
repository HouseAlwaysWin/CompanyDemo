CREATE TABLE CompanyT(
  CompanyID int NOT NULL IDENTITY,
  CompanyName varchar(50) NOT NULL,
  CompanyCode varchar(50)  NOT NULL,
  TaxID varchar(8)  NOT NULL,
  Phone varchar(20) NULL,
  Address varchar(100) NULL,
  WebsiteURL varchar(320) NULL,
  Owner varchar(50) NOT NULL,
  CreatedDate datetime NOT NULL DEFAULT GETUTCDATE(),
  EditedDate datetime NOT NULL DEFAULT GETUTCDATE(),
  CONSTRAINT [PK_dbo.CompanyT] PRIMARY KEY ([CompanyID])
)

CREATE UNIQUE INDEX [CompanyNameIndex] ON [dbo].[CompanyT]([CompanyName])
CREATE UNIQUE INDEX [CompanyCodeIndex] ON [dbo].[CompanyT]([CompanyCode])
CREATE UNIQUE INDEX [TaxIDIndex] ON [dbo].[CompanyT]([TaxID])

CREATE TABLE EmployeeT(
	EmployeeID int NOT NULL IDENTITY,
	EmployeeName varchar(50) NOT NULL,
	Email varchar(320) NULL,
	BirthdayDate datetime NOT NULL,
	SignInDate datetime NOT NULL,
	ResignedDate datetime NULL,
	IsResigned bit NOT NULL Default 0,
	Salary int NOT NULL,
	CreatedDate datetime NOT NULL DEFAULT GETUTCDATE(),
	EditedDate datetime NOT NULL DEFAULT GETUTCDATE(),
	CONSTRAINT [PK_dbo.EmployeeT] PRIMARY KEY ([EmployeeID])
)

CREATE UNIQUE INDEX [EmployeeIDIndex] ON [dbo].[EmployeeT]([EmployeeID])


CREATE TABLE ProductT(
	ProductID int NOT NULL IDENTITY,
	ProductName varchar(100) NOT NULL,
	ProductType varchar(50) NOT NULL,
	Price decimal(2,2) NOT NULL,
	Unit varchar(10) NULL,
	CreatedDate datetime NOT NULL DEFAULT GETUTCDATE(),
	EditedDate datetime NOT NULL DEFAULT GETUTCDATE(),
	CONSTRAINT [PK_dbo.ProductT] PRIMARY KEY ([ProductID])
)

CREATE UNIQUE INDEX [ProductIDIndex] ON [dbo].[ProductT]([ProductID])


CREATE TABLE CompanyT_EmployeeT(
	CompanyID int NOT NULL,
	EmployeeID int NOT NULL ,
	CONSTRAINT [PK_dbo.CompanyT_EmployeeT] PRIMARY KEY ([CompanyID],[EmployeeID])
)

CREATE INDEX [IX_CompanyID] ON [dbo].[CompanyT_EmployeeT]([CompanyID])
CREATE INDEX [IX_EmployeeID] ON [dbo].[CompanyT_EmployeeT]([EmployeeID])

CREATE TABLE CompanyT_ProductT(
	CompanyID int NOT NULL,
	ProductID int NOT NULL ,
)

CREATE INDEX [IX_CompanyID] ON [dbo].[CompanyT_ProductT]([CompanyID])
CREATE INDEX [IX_ProductID] ON [dbo].[CompanyT_ProductT]([ProductID])

ALTER TABLE [dbo].[CompanyT_EmployeeT] ADD CONSTRAINT [FK_dbo.CompanyT_EmployeeT_dbo.CompanyT_CompanyID] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[CompanyT] ([CompanyID]) ON DELETE CASCADE
ALTER TABLE [dbo].[CompanyT_EmployeeT] ADD CONSTRAINT [FK_dbo.CompanyT_EmployeeT_dbo.EmployeeT_EmployeeID] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[EmployeeT] ([EmployeeID]) ON DELETE CASCADE
ALTER TABLE [dbo].[CompanyT_ProductT] ADD CONSTRAINT [FK_dbo.CompanyT_ProductT_dbo.CompanyT_CompanyID] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[CompanyT] ([CompanyID]) ON DELETE CASCADE
ALTER TABLE [dbo].[CompanyT_ProductT] ADD CONSTRAINT [FK_dbo.CompanyT_ProductT_dbo.ProductT_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[ProductT] ([ProductID]) ON DELETE CASCADE




