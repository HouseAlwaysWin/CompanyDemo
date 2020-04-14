using CompanyDemo.Domain.DTOs;
using CompanyDemo.Domain.Entities;
using Dapper;
using DBAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DBAccess.Repositories
{
    internal class ProductTRepository : RepositoryBase, IProductTRepository
    {

        public ProductTRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public void Add(ProductT entity)
        {
            Connection.Execute(@"
                            INSERT INTO ProductT(
                                ProductName,
                                ProductType,
                                Price,
                                Unit)
                            VALUES(
                                @ProductName,
                                @ProductType,
                                @Price,
                                @Unit)
                            DECLARE @LastID int
                            SELECT @LastID =  SCOPE_IDENTITY()

                            INSERT INTO CompanyT_ProductT(
                                CompanyID,
                                ProductID 
                            ) VALUES(
                                @CompanyID,
                                @LastID 
                            )", param: new
            {
                CompanyID = entity.CompanyID,
                ProductName = entity.ProductName,
                ProductType = entity.ProductType,
                Price = entity.Price,
                Unit = entity.Unit
            }, transaction: Transaction);
        }

        public IEnumerable<ProductT> All()
        {
            return Connection.Query<ProductT>("SELECT * FROM ProductT", transaction: Transaction).ToList();
        }

        public void Delete(int id)
        {
            Connection.Execute(
                         @"DELETE FROM ProductT WHERE ProductID = @ProductID
                           DELETE FROM CompanyT_ProductT WHERE ProductID = @ProductID",
                                  param: new { ProductT = id }, transaction: Transaction);
        }

        public void Delete(ProductT entity)
        {
            Connection.Execute(
                         @"DELETE FROM ProductT WHERE ProductID = @ProductID
                           DELETE FROM CompanyT_ProductT WHERE ProductID = @ProductID",
                                  param: new { ProductID = entity.ProductID }, transaction: Transaction);
        }

        public ProductT FindByID(int id)
        {
            return Connection.Query<ProductT>(
                                      "SELECT * FROM ProductT WHERE ProductID = @ProductID",
                                      param: new { ProductID = id }, transaction: Transaction)
                                      .FirstOrDefault();
        }

        public ProductT FindByName(string name)
        {
            return Connection.Query<ProductT>(
                                      @"SELECT * FROM ProductT WHERE ProductName = @ProductName",
                                      param: new { ProductName = name }, transaction: Transaction)
                                      .FirstOrDefault();
        }


        public OneToManyMap<ProductT, CompanyT> FindAllByCompanyID(int companyID, int currentPage, int itemsPerPages, bool isDesc = false)
        {
            var sortType = isDesc ? "DESC" : "ASC";

            var sqlString = @"
                    DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPages
                    SELECT COUNT(*) FROM ProductT WITH(NOLOCK)
                    SELECT	P.ProductID,
                            P.ProductName,		
                            P.ProductType,		
                            P.Price,		
                            P.Unit,		
                            P.CreatedDate,	
                            P.EditedDate	
                        FROM ProductT  AS P 
                        JOIN CompanyT_ProductT AS CP ON CP.ProductID = P.ProductID
                        WHERE CP.CompanyID = @CompanyID
                        ORDER BY   P.ProductID " + sortType + @"
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

            var sqlResult = Connection.QueryMultiple(sqlString,
                new
                {
                    ItemsPerPages = itemsPerPages,
                    CurrentPage = currentPage,
                    SortType = sortType,
                    CompanyID = companyID
                }, transaction: Transaction);
            var result = new OneToManyMap<ProductT, CompanyT>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                List = sqlResult.Read<ProductT>(),
                MapData = sqlResult.ReadFirstOrDefault<CompanyT>()
            };
            return result;
        }

        public void Update(ProductT entity)
        {
            Connection.Execute(
              @"UPDATE ProductT 
                SET ProductName = @ProductName,
                    ProductType = @ProductType,
                    Price = @Price,
                    Unit  = @Unit
                WHERE ProductID = @ProductID",
              param: new
              {
                  ProductID = entity.ProductID,
                  ProductName = entity.ProductName,
                  ProductType = entity.ProductType,
                  Price = entity.Price,
                  Unit = entity.Unit
              }, transaction: Transaction);
        }
    }
}
