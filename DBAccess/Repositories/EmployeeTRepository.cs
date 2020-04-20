using CompanyDemo.Domain.DTOs;
using CompanyDemo.Domain.Entities;
using Dapper;
using DBAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DBAccess.Repositories
{
    internal class EmployeeTRepository : RepositoryBase, IEmployeeTRepository
    {
        public EmployeeTRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public void Add(EmployeeT entity)
        {
            Connection.Execute(@"
                            INSERT INTO EmployeeT(
                                EmployeeName,
                                Email,
                                EmployeePosition,
                                EmployeePhone,
                                BirthdayDate,
                                SignInDate,
                                ResignedDate,
                                IsResigned,
                                Salary)
                            VALUES(
                                @EmployeeName,
                                @Email,
                                @EmployeePhone,
                                @EmployeePosition,
                                @BirthdayDate,
                                @SignInDate,
                                @ResignedDate,
                                @IsResigned,
                                @Salary)

                            DECLARE @LastID int
                            SELECT @LastID =  SCOPE_IDENTITY()

                            INSERT INTO CompanyT_EmployeeT(
                                CompanyID,
                                EmployeeID
                            ) VALUES(
                                @CompanyID,
                                @LastID 
                            )",
                         param: new
                         {
                             EmployeeName = entity.EmployeeName,
                             Email = entity.Email,
                             EmployeePosition = entity.EmployeePosition,
                             EmployeePhone = entity.EmployeePhone,
                             BirthdayDate = entity.BirthdayDate,
                             SignInDate = entity.SignInDate,
                             ResignedDate = entity.ResignedDate,
                             IsResigned = entity.IsResigned,
                             Salary = entity.Salary,
                             CompanyID = entity.CompanyID
                         }, transaction: Transaction);
        }

        public IEnumerable<EmployeeT> All()
        {
            return Connection.Query<EmployeeT>("SELECT * FROM EmployeeT", transaction: Transaction).ToList();
        }

        public void Delete(int id)
        {
            Connection.Execute(
                @"DELETE FROM EmployeeT WHERE EmployeeID = @EmployeeID
                  DELETE FROM CompanyT_EmployeeT WHERE EmployeeID = @EmployeeID",
                         param: new { EmployeeID = id }, transaction: Transaction);
        }

        public void Delete(EmployeeT entity)
        {
            Connection.Execute("" +
                @"DELETE FROM EmployeeT WHERE EmployeeID = @EmployeeID
                  DELETE FROM CompanyT_EmployeeT WHERE EmployeeID = @EmployeeID",
                  param: new { EmployeeID = entity.EmployeeID }, transaction: Transaction);
        }

        public OneToManyMap<EmployeeT, CompanyT> FindAllByCompanyID(int companyID, int currentPage, int itemsPerPages, bool isDesc = false)
        {
            var sortType = isDesc ? "DESC" : "ASC";

            var sqlString = @"
                    DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPages
                    SELECT COUNT(*) 
                        FROM EmployeeT  AS E WITH(NOLOCK)
                        JOIN CompanyT_EmployeeT AS CE ON CE.EmployeeID = E.EmployeeID
                        WHERE CE.CompanyID = @CompanyID 
                    SELECT E.[EmployeeID]
                          ,E.[EmployeeName]
                          ,E.[EmployeePosition]
                          ,E.EmployeePhone
                          ,E.[Email]
                          ,E.[BirthdayDate]
                          ,E.[SignInDate]
                          ,E.[ResignedDate]
                          ,E.[IsResigned]
                          ,E.[Salary]
                          ,E.[CreatedDate]
                          ,E.[EditedDate]
                        FROM EmployeeT  AS E
                        JOIN CompanyT_EmployeeT AS CE ON CE.EmployeeID = E.EmployeeID
                        WHERE CE.CompanyID = @CompanyID
                        ORDER BY   E.EmployeeID " + sortType + @"
                        OFFSET @Start ROWS
                        FETCH NEXT @ItemsPerPages ROWS ONLY
                   SELECT  [CompanyID]
                          ,[CompanyName]
                          ,[CompanyCode]
                          ,[TaxID]
                          ,[Phone]
                          ,[Address]
                          ,[WebsiteURL]
                          ,[Owner]
                          ,[CreatedDate]
                          ,[EditedDate]
                         FROM [dbo].[CompanyT]
                         WHERE CompanyID = @CompanyID
";

            var sqlResult = Connection.QueryMultiple(sqlString,
                new
                {
                    ItemsPerPages = itemsPerPages,
                    CurrentPage = currentPage,
                    SortType = sortType,
                    CompanyID = companyID
                }, transaction: Transaction);
            var result = new OneToManyMap<EmployeeT, CompanyT>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                List = sqlResult.Read<EmployeeT>(),
                MapData = sqlResult.ReadFirstOrDefault<CompanyT>()
            };
            return result;
        }


        public OneToManyMap<EmployeeTAndCompanyT> FindAllByCompanyName(string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            var sortType = isDesc ? "DESC" : "ASC";

            var sqlString = @"
                    DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPage

                    SELECT COUNT(*)  FROM EmployeeID  AS E
                        JOIN CompanyT_EmployeeT AS CE ON CE.EmployeeID = E.EmployeeID
                        JOIN CompanyT AS C ON C.CompanyID =  CE.CompanyID 
                        {0}
                    SELECT	E.EmployeeID,
                            E.EmployeeName,		
                            E.EmployeePosition,		
                            E.EmployeePhone,
                            E.Email,		
                            E.BirthdayDate,		
                            E.SignInDate,		
                            E.ResignedDate,	
                            E.IsResigned,
                            E.Salary,
                            C.CompanyName,
                            C.CompanyCode,
                            C.TaxID,
                            C.Phone,
                            C.Address,
                            C.WebsiteURL,
                            C.Owner
                        FROM EmployeeT  AS P 
                        JOIN CompanyT_EmployeeT AS CE ON CE.EmployeeID = E.EmployeeID
                        JOIN CompanyT AS C ON C.CompanyID =  CE.CompanyID 
                        {0}
                        ORDER BY   E.EmployeeID " + sortType + @"
                        OFFSET @Start ROWS
                        FETCH NEXT @ItemsPerPage ROWS ONLY ";


            if (!string.IsNullOrEmpty(searchText))
            {
                sqlString = string.Format(sqlString, $"WHERE C.CompanyName = @CompanyName");
            }
            else
            {
                sqlString = string.Format(sqlString, string.Empty);
            }

            var sqlResult = Connection.QueryMultiple(sqlString,
                new
                {
                    ItemsPerPage = itemsPerPage,
                    CurrentPage = currentPage,
                    SortType = sortType,
                    CompanyName = searchText
                }, transaction: Transaction);
            var result = new OneToManyMap<EmployeeTAndCompanyT>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                List = sqlResult.Read<EmployeeTAndCompanyT>(),
            };
            return result;
        }


        public OneToManyMap<EmployeeTAndCompanyT> FindAllByEmployeeName(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            var sortType = isDesc ? "DESC" : "ASC";

            var sqlString = @"
                    DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPage

                    SELECT COUNT(*)   FROM EmployeeT  AS E 
                        JOIN CompanyT_EmployeeT AS CE ON CE.EmployeeID = E.EmployeeID
                        JOIN CompanyT AS C ON C.CompanyID =  CE.CompanyID 
                        {0}
                    SELECT	E.EmployeeID,
                            E.EmployeeName,		
                            E.EmployeePosition,		
                            E.EmployeePhone,
                            E.Email,		
                            E.BirthdayDate,		
                            E.SignInDate,		
                            E.ResignedDate,	
                            E.IsResigned,
                            E.Salary,
                            C.CompanyName,
                            C.CompanyCode,
                            C.TaxID,
                            C.Phone,
                            C.Address,
                            C.WebsiteURL,
                            C.Owner
                        FROM EmployeeT  AS E 
                        JOIN CompanyT_EmployeeT AS CE ON CE.EmployeeID = E.EmployeeID
                        JOIN CompanyT AS C ON C.CompanyID =  CE.CompanyID 
                        {0}
                        ORDER BY   E.EmployeeID " + sortType + @"
                        OFFSET @Start ROWS
                        FETCH NEXT @ItemsPerPage ROWS ONLY ";



            if (!string.IsNullOrEmpty(searchText))
            {
                sqlString = string.Format(sqlString, @"WHERE E.EmployeeName = @EmployeeName {0}");
            }

            if (!string.IsNullOrEmpty(companyID) && !string.IsNullOrEmpty(searchText))
            {
                sqlString = string.Format(sqlString, $"AND C.CompanyID = @CompanyID ");
            }
            else if (!string.IsNullOrEmpty(companyID) && string.IsNullOrEmpty(searchText))
            {
                sqlString = string.Format(sqlString, $"WHERE C.CompanyID = @CompanyID ");
            }
            else
            {
                sqlString = string.Format(sqlString, string.Empty);
            }

            var sqlResult = Connection.QueryMultiple(sqlString,
                new
                {
                    ItemsPerPage = itemsPerPage,
                    CurrentPage = currentPage,
                    SortType = sortType,
                    EmployeeName = searchText,
                    CompanyID = companyID
                }, transaction: Transaction);
            var result = new OneToManyMap<EmployeeTAndCompanyT>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                List = sqlResult.Read<EmployeeTAndCompanyT>(),
            };
            return result;
        }

        public OneToManyMap<EmployeeTAndCompanyT> FindAllByEmployeeID(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            var sortType = isDesc ? "DESC" : "ASC";

            var sqlString = @"
                    DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPage

                    SELECT COUNT(*)   FROM EmployeeT  AS E 
                        JOIN CompanyT_EmployeeT AS CE ON CE.EmployeeID = E.EmployeeID
                        JOIN CompanyT AS C ON C.CompanyID =  CE.CompanyID 
                        {0}
                    SELECT	E.EmployeeID,
                            E.EmployeeName,		
                            E.EmployeePosition,		
                            E.EmployeePhone,
                            E.Email,		
                            E.BirthdayDate,		
                            E.SignInDate,		
                            E.ResignedDate,	
                            E.IsResigned,
                            E.Salary,
                            C.CompanyName,
                            C.CompanyCode,
                            C.TaxID,
                            C.Phone,
                            C.Address,
                            C.WebsiteURL,
                            C.Owner
                        FROM EmployeeT  AS E 
                        JOIN CompanyT_EmployeeT AS CE ON CE.EmployeeID = E.EmployeeID
                        JOIN CompanyT AS C ON C.CompanyID =  CE.CompanyID 
                        {0}
                        ORDER BY   E.EmployeeID " + sortType + @"
                        OFFSET @Start ROWS
                        FETCH NEXT @ItemsPerPage ROWS ONLY ";



            int employeeId;
            bool isInt = int.TryParse(searchText, out employeeId);

            if (!string.IsNullOrEmpty(searchText))
            {
                if (!isInt)
                {
                    return new OneToManyMap<EmployeeTAndCompanyT>
                    {
                        TotalCount = 0,
                        List = new List<EmployeeTAndCompanyT>(),
                    };
                }
                sqlString = string.Format(sqlString, @"WHERE E.EmployeeID = @EmployeeID {0}");
            }


            if (!string.IsNullOrEmpty(companyID) && isInt)
            {
                sqlString = string.Format(sqlString, $"AND C.CompanyID = @CompanyID ");
            }
            else if (!string.IsNullOrEmpty(companyID) && string.IsNullOrEmpty(searchText))
            {
                sqlString = string.Format(sqlString, $"WHERE C.CompanyID = @CompanyID ");
            }
            else
            {
                sqlString = string.Format(sqlString, string.Empty);
            }

            var sqlResult = Connection.QueryMultiple(sqlString,
                new
                {
                    ItemsPerPage = itemsPerPage,
                    CurrentPage = currentPage,
                    SortType = sortType,
                    EmployeeID = searchText,
                    CompanyID = companyID
                }, transaction: Transaction);
            var result = new OneToManyMap<EmployeeTAndCompanyT>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                List = sqlResult.Read<EmployeeTAndCompanyT>(),
            };
            return result;
        }

        public OneToManyMap<EmployeeTAndCompanyT> FindAllByEmployeePhone(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            var sortType = isDesc ? "DESC" : "ASC";

            var sqlString = @"
                    DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPage

                    SELECT COUNT(*)   FROM EmployeeT  AS E 
                        JOIN CompanyT_EmployeeT AS CE ON CE.EmployeeID = E.EmployeeID
                        JOIN CompanyT AS C ON C.CompanyID =  CE.CompanyID 
                        {0}
                    SELECT	E.EmployeeID,
                            E.EmployeeName,		
                            E.EmployeePosition,		
                            E.EmployeePhone,
                            E.Email,		
                            E.BirthdayDate,		
                            E.SignInDate,		
                            E.ResignedDate,	
                            E.IsResigned,
                            E.Salary,
                            C.CompanyName,
                            C.CompanyCode,
                            C.TaxID,
                            C.Phone,
                            C.Address,
                            C.WebsiteURL,
                            C.Owner
                        FROM EmployeeT  AS E 
                        JOIN CompanyT_EmployeeT AS CE ON CE.EmployeeID = E.EmployeeID
                        JOIN CompanyT AS C ON C.CompanyID =  CE.CompanyID 
                        {0}
                        ORDER BY   E.EmployeeID " + sortType + @"
                        OFFSET @Start ROWS
                        FETCH NEXT @ItemsPerPage ROWS ONLY ";



            if (!string.IsNullOrEmpty(searchText))
            {
                sqlString = string.Format(sqlString, @"WHERE E.EmployeePhone = @EmployeePhone {0}");
            }

            if (!string.IsNullOrEmpty(companyID) && !string.IsNullOrEmpty(searchText))
            {
                sqlString = string.Format(sqlString, $"AND C.CompanyID = @CompanyID ");
            }
            else if (!string.IsNullOrEmpty(companyID) && string.IsNullOrEmpty(searchText))
            {
                sqlString = string.Format(sqlString, $"WHERE C.CompanyID = @CompanyID ");
            }
            else
            {
                sqlString = string.Format(sqlString, string.Empty);
            }

            var sqlResult = Connection.QueryMultiple(sqlString,
                new
                {
                    ItemsPerPage = itemsPerPage,
                    CurrentPage = currentPage,
                    SortType = sortType,
                    EmployeePhone = searchText,
                    CompanyID = companyID
                }, transaction: Transaction);
            var result = new OneToManyMap<EmployeeTAndCompanyT>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                List = sqlResult.Read<EmployeeTAndCompanyT>(),
            };
            return result;
        }

        public OneToManyMap<EmployeeTAndCompanyT> FindAllByEmployeePosition(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            var sortType = isDesc ? "DESC" : "ASC";

            var sqlString = @"
                    DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPage

                    SELECT COUNT(*)   FROM EmployeeT  AS E 
                        JOIN CompanyT_EmployeeT AS CE ON CE.EmployeeID = E.EmployeeID
                        JOIN CompanyT AS C ON C.CompanyID =  CE.CompanyID 
                        {0}
                    SELECT	E.EmployeeID,
                            E.EmployeeName,		
                            E.EmployeePosition,		
                            E.EmployeePhone,
                            E.Email,		
                            E.BirthdayDate,		
                            E.SignInDate,		
                            E.ResignedDate,	
                            E.IsResigned,
                            E.Salary,
                            C.CompanyName,
                            C.CompanyCode,
                            C.TaxID,
                            C.Phone,
                            C.Address,
                            C.WebsiteURL,
                            C.Owner
                        FROM EmployeeT  AS E 
                        JOIN CompanyT_EmployeeT AS CE ON CE.EmployeeID = E.EmployeeID
                        JOIN CompanyT AS C ON C.CompanyID =  CE.CompanyID 
                        {0}
                        ORDER BY   E.EmployeeID " + sortType + @"
                        OFFSET @Start ROWS
                        FETCH NEXT @ItemsPerPage ROWS ONLY ";



            if (!string.IsNullOrEmpty(searchText))
            {
                sqlString = string.Format(sqlString, @"WHERE E.EmployeePosition = @EmployeePosition {0}");
            }

            if (!string.IsNullOrEmpty(companyID) && !string.IsNullOrEmpty(searchText))
            {
                sqlString = string.Format(sqlString, $"AND C.CompanyID = @CompanyID ");
            }
            else if (!string.IsNullOrEmpty(companyID) && string.IsNullOrEmpty(searchText))
            {
                sqlString = string.Format(sqlString, $"WHERE C.CompanyID = @CompanyID ");
            }
            else
            {
                sqlString = string.Format(sqlString, string.Empty);
            }

            var sqlResult = Connection.QueryMultiple(sqlString,
                new
                {
                    ItemsPerPage = itemsPerPage,
                    CurrentPage = currentPage,
                    SortType = sortType,
                    EmployeePosition = searchText,
                    CompanyID = companyID
                }, transaction: Transaction);
            var result = new OneToManyMap<EmployeeTAndCompanyT>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                List = sqlResult.Read<EmployeeTAndCompanyT>(),
            };
            return result;
        }

        public OneToManyMap<EmployeeTAndCompanyT> FindAllByEmployeeBirthday(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            var sortType = isDesc ? "DESC" : "ASC";

            var sqlString = @"
                    DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPage

                    SELECT COUNT(*)   FROM EmployeeT  AS E 
                        JOIN CompanyT_EmployeeT AS CE ON CE.EmployeeID = E.EmployeeID
                        JOIN CompanyT AS C ON C.CompanyID =  CE.CompanyID 
                        {0}
                    SELECT	E.EmployeeID,
                            E.EmployeeName,		
                            E.EmployeePosition,		
                            E.EmployeePhone,
                            E.Email,		
                            E.BirthdayDate,		
                            E.SignInDate,		
                            E.ResignedDate,	
                            E.IsResigned,
                            E.Salary,
                            C.CompanyName,
                            C.CompanyCode,
                            C.TaxID,
                            C.Phone,
                            C.Address,
                            C.WebsiteURL,
                            C.Owner
                        FROM EmployeeT  AS E 
                        JOIN CompanyT_EmployeeT AS CE ON CE.EmployeeID = E.EmployeeID
                        JOIN CompanyT AS C ON C.CompanyID =  CE.CompanyID 
                        {0}
                        ORDER BY   E.EmployeeID " + sortType + @"
                        OFFSET @Start ROWS
                        FETCH NEXT @ItemsPerPage ROWS ONLY ";



            if (!string.IsNullOrEmpty(searchText))
            {
                sqlString = string.Format(sqlString, @"WHERE E.BirthdayDate = @BirthdayDate {0}");
            }

            if (!string.IsNullOrEmpty(companyID) && !string.IsNullOrEmpty(searchText))
            {
                sqlString = string.Format(sqlString, $"AND C.CompanyID = @CompanyID ");
            }
            else if (!string.IsNullOrEmpty(companyID) && string.IsNullOrEmpty(searchText))
            {
                sqlString = string.Format(sqlString, $"WHERE C.CompanyID = @CompanyID ");
            }
            else
            {
                sqlString = string.Format(sqlString, string.Empty);
            }

            var sqlResult = Connection.QueryMultiple(sqlString,
                new
                {
                    ItemsPerPage = itemsPerPage,
                    CurrentPage = currentPage,
                    SortType = sortType,
                    BirthdayDate = searchText,
                    CompanyID = companyID
                }, transaction: Transaction);
            var result = new OneToManyMap<EmployeeTAndCompanyT>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                List = sqlResult.Read<EmployeeTAndCompanyT>(),
            };
            return result;
        }

        public OneToManyMap<EmployeeT> FindAllByName(int currentPage, int itemsPerPages, string searchText, bool isDesc = false, string sortBy = "EmployeeID")
        {
            throw new NotImplementedException();
        }

        public EmployeeT FindByID(int id)
        {
            return Connection.Query<EmployeeT>(
                           "SELECT * FROM EmployeeT WHERE EmployeeID = @EmployeeID",
                           param: new { EmployeeID = id }, transaction: Transaction)
                           .FirstOrDefault();
        }

        public EmployeeT FindByName(string name)
        {
            return Connection.Query<EmployeeT>(
                                       "SELECT * FROM EmployeeT WHERE EmployeeName = @EmployeeName",
                                       param: new { EmployeeName = name }, transaction: Transaction)
                                       .FirstOrDefault();
        }

        public void Update(EmployeeT entity)
        {
            Connection.Execute(
               @"UPDATE EmployeeT SET 
                        EmployeeName = @EmployeeName,
                        EmployeePosition = @EmployeePosition,
                        EmployeePhone = @EmployeePhone,
                        Email = @Email,
                        BirthdayDate = @BirthdayDate,
                        SignInDate = @SignInDate,
                        ResignedDate = @ResignedDate,
                        IsResigned = @IsResigned,
                        Salary = @Salary,
                        EditedDate = GETUTCDATE() 
                    WHERE EmployeeID = @EmployeeID",
               param: new
               {
                   EmployeeID = entity.EmployeeID,
                   EmployeePosition = entity.EmployeePosition,
                   EmployeePhone = entity.EmployeePhone,
                   EmployeeName = entity.EmployeeName,
                   Email = entity.Email,
                   BirthdayDate = entity.BirthdayDate,
                   SignInDate = entity.SignInDate,
                   ResignedDate = entity.ResignedDate,
                   IsResigned = entity.IsResigned,
                   Salary = entity.Salary,
               }, transaction: Transaction);
        }
    }
}
