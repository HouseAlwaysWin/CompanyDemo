
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