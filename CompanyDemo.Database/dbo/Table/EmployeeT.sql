
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

GO
CREATE UNIQUE INDEX [EmployeeIDIndex] ON [dbo].[EmployeeT]([EmployeeID])
