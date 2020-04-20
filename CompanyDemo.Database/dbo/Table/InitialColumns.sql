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

CREATE TABLE EmployeeT(
	EmployeeID int NOT NULL IDENTITY,
	EmployeeName varchar(50) NOT NULL,
	EmployeePosition varchar(50) NOT  NULL,
	EmployeePhone varchar(20)  NULL,
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

GO
CREATE UNIQUE INDEX [EmployeeIDIndex] ON [dbo].[EmployeeT]([EmployeeID])

CREATE TABLE [dbo].[Member] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
	[MemberType]		   INT			  NOT NULL DEFAULT(1),
	[IsLogined]	           bit NOT NULL DEFAULT(0)
    CONSTRAINT [PK_dbo.Member] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [MemberNameIndex]
    ON [dbo].[Member]([UserName] ASC);



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


CREATE TABLE [dbo].[Role] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.Role] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[Role]([Name] ASC);


CREATE TABLE CompanyT_EmployeeT(
	CompanyID int NOT NULL,
	EmployeeID int NOT NULL ,
	CONSTRAINT [PK_dbo.CompanyT_EmployeeT] PRIMARY KEY ([CompanyID],[EmployeeID])
)

GO
CREATE INDEX [IX_CompanyID] ON [dbo].[CompanyT_EmployeeT]([CompanyID])
GO
CREATE INDEX [IX_EmployeeID] ON [dbo].[CompanyT_EmployeeT]([EmployeeID])
GO
ALTER TABLE [dbo].[CompanyT_EmployeeT] ADD CONSTRAINT [FK_dbo.CompanyT_EmployeeT_dbo.CompanyT_CompanyID] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[CompanyT] ([CompanyID]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CompanyT_EmployeeT] ADD CONSTRAINT [FK_dbo.CompanyT_EmployeeT_dbo.EmployeeT_EmployeeID] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[EmployeeT] ([EmployeeID]) ON DELETE CASCADE


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

CREATE TABLE [dbo].[MemberLogin] (
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [MemberId]      INT            NOT NULL,
    CONSTRAINT [PK_dbo.MemberLogin] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [MemberId] ASC),
    CONSTRAINT [FK_dbo.MemberLogin_dbo.Member_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Member] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_MemberId]
    ON [dbo].[MemberLogin]([MemberId] ASC);

CREATE TABLE [dbo].[MemberClaim] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [MemberId]   INT            NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.MemberClaim] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.MemberClaim_dbo.Member_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Member] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_MemberId]
    ON [dbo].[MemberClaim]([MemberId] ASC);

CREATE TABLE [dbo].[MemberRole] (
    [MemberId] INT NOT NULL,
    [RoleId]   INT NOT NULL,
    CONSTRAINT [PK_dbo.MemberRoles] PRIMARY KEY CLUSTERED ([MemberId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.MemberRoles_dbo.Member_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Member] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.MemberRoles_dbo.Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_MemberId]
    ON [dbo].[MemberRole]([MemberId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [dbo].[MemberRole]([RoleId] ASC);


