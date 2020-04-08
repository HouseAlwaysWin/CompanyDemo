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

        public EntityWithTotalCount<CompanyT> FindAllByPagination(int currentPage, int itemsPerPages, bool isDesc = false, string sortBy = "CompanyID")
        {
            var sortType = isDesc ? "DESC" : "ASC";

            var sqlResult = Connection.QueryMultiple(@"
                    DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPages
                    SELECT COUNT(*) FROM CompanyT WITH(NOLOCK)
                    SELECT * FROM CompanyT 
                        ORDER BY " + sortBy + " " + sortType + @"
                        OFFSET @Start ROWS
                        FETCH NEXT @ItemsPerPages ROWS ONLY",
                new
                {
                    ItemsPerPages = itemsPerPages,
                    CurrentPage = currentPage,
                }, transaction: Transaction);
            var result = new EntityWithTotalCount<CompanyT>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                List = sqlResult.Read<CompanyT>()
            };
            return result;
        }

        public void Delete(int id)
        {
            Connection.Execute("DELETE FROM CompanyT WHERE CompanyID = @CompanyID",
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
