DELETE FROM  [dbo].[Member]
DBCC CHECKIDENT ([Member], RESEED, 0)

INSERT INTO  [dbo].[Member]
  ([Email]
      ,[EmailConfirmed]
      ,[PasswordHash]
      ,[SecurityStamp]
      ,[PhoneNumber]
      ,[PhoneNumberConfirmed]
      ,[TwoFactorEnabled]
      ,[LockoutEndDateUtc]
      ,[LockoutEnabled]
      ,[AccessFailedCount]
      ,[UserName]
      ,[MemberType]
      ,[IsLogined]) VALUES(
	  'admin@a.com',
	  0,
	  'ALWu0Vp+nP1/T4Xe18E8hmOktdfF4P/yZqx6nXbvUZ3kyzQ6YG+0UqOAr38GR4ZfNA==',
	  'a48b20e6-8dc2-4bec-80f7-81969d51a4bd',
	  NULL,
	  0,
	  0,
	  NULL,
	  0,
	  0,
	  'admin@a.com',
	  2,
	  0
	  )

DELETE FROM  [dbo].[Role]
DBCC CHECKIDENT ([Role], RESEED, 0)

INSERT INTO [dbo].[Role] ([Name]) VALUES ('管理者')
INSERT INTO [dbo].[Role] ([Name]) VALUES ('顯示公司列表')
INSERT INTO [dbo].[Role] ([Name]) VALUES ('顯示公司查詢')
INSERT INTO [dbo].[Role] ([Name]) VALUES ('顯示員工查詢')
INSERT INTO [dbo].[Role] ([Name]) VALUES ('顯示員工列表')
INSERT INTO [dbo].[Role] ([Name]) VALUES ('顯示產品查詢')
INSERT INTO [dbo].[Role] ([Name]) VALUES ('顯示產品列表')


DELETE FROM  [dbo].[CompanyT]
DBCC CHECKIDENT ([CompanyT], RESEED, 0)

INSERT INTO [dbo].[CompanyT] ([CompanyName],[CompanyCode],[TaxID],[Phone],[Address],[WebsiteURL] ,[Owner]) VALUES('公司A','123456','12345678','0912345670','地址A','網址A','擁有者A')
INSERT INTO [dbo].[CompanyT] ([CompanyName],[CompanyCode],[TaxID],[Phone],[Address],[WebsiteURL] ,[Owner]) VALUES('公司B','123455','12345679','0912345671','地址B','網址B','擁有者B')
INSERT INTO [dbo].[CompanyT] ([CompanyName],[CompanyCode],[TaxID],[Phone],[Address],[WebsiteURL] ,[Owner]) VALUES('公司C','123454','12345670','0912345672','地址C','網址C','擁有者C')
INSERT INTO [dbo].[CompanyT] ([CompanyName],[CompanyCode],[TaxID],[Phone],[Address],[WebsiteURL] ,[Owner]) VALUES('公司D','123453','12345671','0912345673','地址D','網址D','擁有者D')
INSERT INTO [dbo].[CompanyT] ([CompanyName],[CompanyCode],[TaxID],[Phone],[Address],[WebsiteURL] ,[Owner]) VALUES('公司E','123452','12345672','0912345674','地址E','網址E','擁有者E')
INSERT INTO [dbo].[CompanyT] ([CompanyName],[CompanyCode],[TaxID],[Phone],[Address],[WebsiteURL] ,[Owner]) VALUES('公司F','123451','12345673','0912345675','地址F','網址F','擁有者F')
INSERT INTO [dbo].[CompanyT] ([CompanyName],[CompanyCode],[TaxID],[Phone],[Address],[WebsiteURL] ,[Owner]) VALUES('公司G','123450','12345674','0912345676','地址G','網址G','擁有者G')
INSERT INTO [dbo].[CompanyT] ([CompanyName],[CompanyCode],[TaxID],[Phone],[Address],[WebsiteURL] ,[Owner]) VALUES('公司H','123459','12345675','0912345677','地址H','網址H','擁有者H')
INSERT INTO [dbo].[CompanyT] ([CompanyName],[CompanyCode],[TaxID],[Phone],[Address],[WebsiteURL] ,[Owner]) VALUES('公司I','123458','12345676','0912345678','地址I','網址I','擁有者I')
INSERT INTO [dbo].[CompanyT] ([CompanyName],[CompanyCode],[TaxID],[Phone],[Address],[WebsiteURL] ,[Owner]) VALUES('公司J','123457','12345677','0912345679','地址J','網址J','擁有者J')


DELETE FROM  [dbo].[EmployeeT]
DBCC CHECKIDENT ([EmployeeT], RESEED, 0)

INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工A','職位A','0912345670','a@a.com','2020-04-01 00:00:00.000',GETDATE(),NULL,0,123451)
INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工B','職位B','0912345671','b@a.com','2020-04-02 00:00:00.000',GETDATE(),NULL,0,123452)
INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工C','職位C','0912345672','c@a.com','2020-04-03 00:00:00.000',GETDATE(),NULL,0,123453)
INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工D','職位D','0912345673','d@a.com','2020-04-04 00:00:00.000',GETDATE(),NULL,0,123454)
INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工E','職位E','0912345674','e@a.com','2020-04-05 00:00:00.000',GETDATE(),NULL,0,123455)
INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工F','職位F','0912345675','f@a.com','2020-04-06 00:00:00.000',GETDATE(),NULL,0,123456)
INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工G','職位G','0912345676','g@a.com','2020-04-07 00:00:00.000',GETDATE(),NULL,0,123457)
INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工H','職位H','0912345677','h@a.com','2020-04-08 00:00:00.000',GETDATE(),NULL,0,123458)
INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工I','職位I','0912345678','i@a.com','2020-04-09 00:00:00.000',GETDATE(),NULL,0,123459)
INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工J','職位J','0912345679','j@a.com','2020-04-10 00:00:00.000',GETDATE(),NULL,0,123450)

INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工AA','職位A','0912345670','aa@a.com','2020-04-01 00:00:00.000',GETDATE(),NULL,0,123451)
INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工BB','職位B','0912345671','bb@a.com','2020-04-02 00:00:00.000',GETDATE(),NULL,0,123452)
INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工CC','職位C','0912345672','cc@a.com','2020-04-03 00:00:00.000',GETDATE(),NULL,0,123453)
INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工DD','職位D','0912345673','dd@a.com','2020-04-04 00:00:00.000',GETDATE(),NULL,0,123454)
INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工EE','職位E','0912345674','ee@a.com','2020-04-05 00:00:00.000',GETDATE(),NULL,0,123455)
INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工FF','職位F','0912345675','ff@a.com','2020-04-06 00:00:00.000',GETDATE(),NULL,0,123456)
INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工GG','職位G','0912345676','gg@a.com','2020-04-07 00:00:00.000',GETDATE(),NULL,0,123457)
INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工HH','職位H','0912345677','hh@a.com','2020-04-08 00:00:00.000',GETDATE(),NULL,0,123458)
INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工II','職位I','0912345678','ii@a.com','2020-04-09 00:00:00.000',GETDATE(),NULL,0,123459)
INSERT INTO [dbo].[EmployeeT] ([EmployeeName],[EmployeePosition],[EmployeePhone],[Email],[BirthdayDate],[SignInDate],[ResignedDate],[IsResigned],[Salary]) VALUES ('員工JJ','職位J','0912345679','jj@a.com','2020-04-10 00:00:00.000',GETDATE(),NULL,0,123450)


DELETE FROM  [dbo].[ProductT]
DBCC CHECKIDENT ([ProductT], RESEED, 0)

INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品A','類型A',120,'單位A')
INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品B','類型B',121,'單位B')
INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品C','類型C',122,'單位C')
INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品D','類型E',123,'單位D')
INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品E','類型E',124,'單位E')
INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品F','類型F',125,'單位F')
INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品G','類型G',126,'單位G')
INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品H','類型H',127,'單位H')
INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品I','類型I',128,'單位I')
INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品J','類型J',129,'單位J')

INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品AA','類型A',120,'單位A')
INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品BB','類型B',121,'單位B')
INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品CC','類型C',122,'單位C')
INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品DD','類型E',123,'單位D')
INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品EE','類型E',124,'單位E')
INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品FF','類型F',125,'單位F')
INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品GG','類型G',126,'單位G')
INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品HH','類型H',127,'單位H')
INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品II','類型I',128,'單位I')
INSERT INTO [dbo].[ProductT] ([ProductName],[ProductType],[Price],[Unit]) VALUES ('產品JJ','類型J',129,'單位J')



INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (1,1)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (1,2)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (2,3)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (2,4)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (3,5)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (3,6)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (4,7)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (4,8)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (5,9)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (5,10)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (6,11)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (6,12)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (7,13)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (7,14)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (8,15)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (8,16)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (9,17)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (9,18)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (10,19)
INSERT INTO [dbo].[CompanyT_ProductT] ([CompanyID],[ProductID]) VALUES (10,20)

INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (1,1)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (1,2)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (2,3)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (2,4)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (3,5)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (3,6)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (4,7)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (4,8)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (5,9)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (5,10)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (6,11)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (6,12)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (7,13)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (7,14)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (8,15)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (8,16)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (9,17)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (9,18)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (10,19)
INSERT INTO [dbo].[CompanyT_EmployeeT] ([CompanyID],[EmployeeID]) VALUES (10,20)


INSERT INTO [dbo].[MemberRole] ([MemberId] ,[RoleId]) VALUES (1,1)
INSERT INTO [dbo].[MemberRole] ([MemberId] ,[RoleId]) VALUES (1,2)
INSERT INTO [dbo].[MemberRole] ([MemberId] ,[RoleId]) VALUES (1,3)
INSERT INTO [dbo].[MemberRole] ([MemberId] ,[RoleId]) VALUES (1,4)
INSERT INTO [dbo].[MemberRole] ([MemberId] ,[RoleId]) VALUES (1,5)
INSERT INTO [dbo].[MemberRole] ([MemberId] ,[RoleId]) VALUES (1,6)
INSERT INTO [dbo].[MemberRole] ([MemberId] ,[RoleId]) VALUES (1,7)

