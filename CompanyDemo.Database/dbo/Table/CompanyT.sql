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

GO
CREATE UNIQUE INDEX [CompanyNameIndex] ON [dbo].[CompanyT]([CompanyName])
GO
CREATE UNIQUE INDEX [CompanyCodeIndex] ON [dbo].[CompanyT]([CompanyCode])
GO
CREATE UNIQUE INDEX [TaxIDIndex] ON [dbo].[CompanyT]([TaxID])