CREATE TABLE CompanyT(
  CompanyID int NOT NULL IDENTITY CONSTRAINT PK_CompanyT_CompanyID PRIMARY KEY CLUSTERED,
  CompanyName varchar(50) UNIQUE NOT NULL,
  CompanyCode varchar(50) UNIQUE NOT NULL,
  TaxID varchar(8) UNIQUE NOT NULL,
  Phone varchar(20) NULL,
  Address varchar(100) NULL,
  WebsiteURL varchar(320) NULL,
  Owner varchar(50) NOT NULL,
  CreatedDate datetime NOT NULL DEFAULT GETUTCDATE(),
  EditedDate datetime NOT NULL DEFAULT GETUTCDATE()
)

CREATE TABLE EmployeeT(
	EmployeeID int NOT NULL IDENTITY CONSTRAINT PK_EmployeeT_EmployeeID PRIMARY KEY CLUSTERED,
	EmployeeName varchar(50) NOT NULL,
	Email varchar(320) NULL,
	BirthdayDate datetime NOT NULL,
	SignInDate datetime NOT NULL,
	ResignedDate datetime NULL,
	IsResigned bit NOT NULL Default 0,
	Salary int NOT NULL,
	CreatedDate datetime NOT NULL DEFAULT GETUTCDATE(),
	EditedDate datetime NOT NULL DEFAULT GETUTCDATE()
)

CREATE TABLE ProductT(
	ProductID int NOT NULL IDENTITY CONSTRAINT PK_ProductT_ProductID PRIMARY KEY CLUSTERED,
	ProductName varchar(100) NOT NULL,
	ProductType varchar(50) NOT NULL,
	Price decimal(2,2) NOT NULL,
	Unit varchar(10) NULL,
	CreatedDate datetime NOT NULL DEFAULT GETUTCDATE(),
	EditedDate datetime NOT NULL DEFAULT GETUTCDATE()
)

CREATE TABLE CompanyT_EmployeeT(
	SN int NOT NULL IDENTITY PRIMARY KEY CLUSTERED,
	CompanyID int NOT NULL,
	EmployeeID int NOT NULL ,
)

CREATE TABLE CompanyT_ProductT(
	SN int NOT NULL IDENTITY PRIMARY KEY CLUSTERED,
	CompanyID int NOT NULL,
	ProductID int NOT NULL ,
)

--DROP TABLE CompanyT
--DROP TABLE EmployeeT
--DROP TABLE ProductT
--DROP TABLE CompanyT_EmployeeT
--DROP TABLE CompanyT_ProductT