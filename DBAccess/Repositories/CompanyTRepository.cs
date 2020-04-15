using CompanyDemo.Domain.DTOs;
using CompanyDemo.Domain.Entities;
using Dapper;
using DBAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DBAccess.Repositories
{
    internal class CompanyTRepository : RepositoryBase, ICompanyTRepository
    {
        public CompanyTRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public void Add(CompanyT entity)
        {
            Connection.Execute(@"INSERT INTO CompanyT(
                                CompanyName,
                                CompanyCode,
                                TaxID,
                                Phone,
                                Address,
                                WebsiteURL,
                                Owner)
                            VALUES(
                                @CompanyName,
                                @CompanyCode,
                                @TaxID,
                                @Phone,
                                @Address,
                                @WebsiteURL,
                                @Owner 
                            )",
                            param: new
                            {
                                CompanyName = entity.CompanyName,
                                CompanyCode = entity.CompanyCode,
                                TaxID = entity.TaxID,
                                Phone = entity.Phone,
                                Address = entity.Address,
                                WebsiteURL = entity.WebsiteURL,
                                Owner = entity.Owner
                            }, transaction: Transaction);
        }

        /// <summary>
        /// 取得全部公司列表(1000筆)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CompanyT> All()
        {
            return Connection.Query<CompanyT>("SELECT TOP(1000) * FROM CompanyT", transaction: Transaction).ToList();
        }

        public OneToManyMap<CompanyT> FindAllByID(int currentPage, int itemsPerPage, int? searchText = null, bool isDesc = false)
        {
            var sortType = isDesc ? "DESC" : "ASC";

            var sqlString = @"
                    DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPage
                    SELECT COUNT(*) FROM CompanyT WITH(NOLOCK)
                        {0}
                    SELECT * FROM CompanyT 
                        {0}
                        ORDER BY   CompanyID " + sortType + @"
                        OFFSET @Start ROWS
                        FETCH NEXT @ItemsPerPage ROWS ONLY";

            if (searchText != null)
            {
                sqlString = string.Format(sqlString, $" WHERE  CompanyID = @SearchText");
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
                    SearchText = searchText,
                    SortType = sortType
                }, transaction: Transaction);
            var result = new OneToManyMap<CompanyT>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                List = sqlResult.Read<CompanyT>()
            };
            return result;
        }

        public OneToManyMap<CompanyT> FindAllByName(int currentPage, int itemsPerPage, string searchText, bool isDesc = false)
        {
            var sortType = isDesc ? "DESC" : "ASC";

            var sqlString = @"
                    DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPage
                    SELECT COUNT(*) FROM CompanyT WITH(NOLOCK)
                        {0}
                    SELECT * FROM CompanyT 
                        {0}
                        ORDER BY   CompanyID  " + sortType + @"
                        OFFSET @Start ROWS
                        FETCH NEXT @ItemsPerPage ROWS ONLY";

            if (!string.IsNullOrEmpty(searchText))
            {
                sqlString = string.Format(sqlString, $" WHERE  CompanyName = @SearchText");
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
                    SearchText = searchText,
                    SortType = sortType
                }, transaction: Transaction);
            var result = new OneToManyMap<CompanyT>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                List = sqlResult.Read<CompanyT>()
            };
            return result;
        }

        public void Delete(int id)
        {
            Connection.Execute(@"
              DELETE FROM [EmployeeT] WHERE EmployeeID IN (
              SELECT E.EmployeeID
              FROM [CompanyDB].[dbo].[CompanyT_EmployeeT] AS CE
              JOIN [CompanyDB].[dbo].[EmployeeT] AS E ON CE.EmployeeID = E.[EmployeeID]
              WHERE CE.CompanyID  = @CompanyID)
              DELETE FROM CompanyT WHERE CompanyID = @CompanyID
                ",
               param: new { CompanyID = id }, transaction: Transaction);
        }

        public void Delete(CompanyT entity)
        {
            Delete(entity.CompanyID);
        }

        public CompanyT FindByID(int id)
        {
            return Connection.Query<CompanyT>(
                "SELECT * FROM CompanyT WHERE CompanyID = @CompanyID",
                param: new { CompanyID = id }, transaction: Transaction)
                .FirstOrDefault();
        }

        public CompanyT FindByName(string name)
        {
            return Connection.Query<CompanyT>(
                          "SELECT * FROM CompanyT WHERE CompanyName = @CompanyName",
                          param: new { CompanyName = name }, transaction: Transaction)
                          .FirstOrDefault();
        }

        public void Update(CompanyT entity)
        {
            Connection.Execute(
               @"UPDATE CompanyT SET 
                        CompanyName = @CompanyName,
                        CompanyCode = @CompanyCode,
                        TaxID = @TaxID,
                        Phone = @Phone,
                        Address = @Address,
                        WebsiteURL = @WebsiteURL,
                        Owner = @Owner,
                        EditedDate = GETUTCDATE()
                    WHERE CompanyID = @CompanyID",
               param: new
               {
                   CompanyID = entity.CompanyID,
                   CompanyName = entity.CompanyName,
                   CompanyCode = entity.CompanyCode,
                   TaxID = entity.TaxID,
                   Phone = entity.Phone,
                   Address = entity.Address,
                   WebsiteURL = entity.WebsiteURL,
                   Owner = entity.Owner,
               }, transaction: Transaction);
        }
    }
}
