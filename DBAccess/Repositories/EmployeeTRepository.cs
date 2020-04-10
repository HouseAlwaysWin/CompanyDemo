using Dapper;
using DBAccess.DTO;
using DBAccess.Entities;
using DBAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

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
                                BirthdayDate,
                                SignInDate,
                                ResignedDate,
                                IsResigned,
                                Salary)
                            VALUES(
                                @EmployeeName,
                                @Email,
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
                             BirthdayDate = entity.BrithdayDate,
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
            Connection.Execute("" +
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

        public EntityWithTotalCount<EmployeeT, CompanyT> FindAllByCompanyID(int companyID, int currentPage, int itemsPerPages, int? searchText, bool isDesc = false)
        {
            var sortType = isDesc ? "DESC" : "ASC";

            var sqlString = @"
                    DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPages
                    SELECT COUNT(*) FROM EmployeeT WITH(NOLOCK)
                    SELECT E.[EmployeeID]
                          ,E.[EmployeeName]
                          ,E.[Email]
                          ,E.[BirthdayDate]
                          ,E.[SignInDate]
                          ,E.[ResignedDate]
                          ,E.[IsResigned]
                          ,E.[Salary]
                          ,E.[CreatedDate]
                          ,E.[EditedDate]
                        FROM EmployeeT  AS E
                        LEFT JOIN CompanyT_EmployeeT AS CE ON CE.CompanyID = @CompanyID
                        {0}
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
                         FROM [CompanyDB].[dbo].[CompanyT]
                         WHERE CompanyID = @CompanyID
";

            if (searchText != null)
            {
                sqlString = string.Format(sqlString, $" WHERE  E.EmployeeID = @SearchText");
            }
            else
            {
                sqlString = string.Format(sqlString, string.Empty);
            }

            var sqlResult = Connection.QueryMultiple(sqlString,
                new
                {
                    ItemsPerPages = itemsPerPages,
                    CurrentPage = currentPage,
                    SearchText = searchText,
                    SortType = sortType,
                    CompanyID = companyID
                }, transaction: Transaction);
            var result = new EntityWithTotalCount<EmployeeT, CompanyT>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                List = sqlResult.Read<EmployeeT>(),
                MapData = sqlResult.ReadFirstOrDefault<CompanyT>()
            };
            return result;
        }

        public EntityWithTotalCount<EmployeeT> FindAllByName(int currentPage, int itemsPerPages, string searchText, bool isDesc = false, string sortBy = "EmployeeID")
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
                        Email = @Email,
                        BirthdayDate = @BirthdayDate,
                        SignInDate = @SignInDate,
                        ResignedDate = @ResignedDate,
                        IsResigned = @IsResigned,
                        Salary = @Salary,
                        EditedDate = GETUTCDATE() 
                    WHERE CompanyID = @CompanyID",
               param: new
               {
                   EmployeeName = entity.EmployeeName,
                   Email = entity.Email,
                   BirthdayDate = entity.BrithdayDate,
                   SignInDate = entity.SignInDate,
                   ResignedDate = entity.ResignedDate,
                   IsRegistered = entity.IsResigned,
                   Salary = entity.Salary,
               }, transaction: Transaction);
        }
    }
}
