using Dapper;
using DBAccess.Entities;
using DBAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

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
                           DELETE FROM CompanyT_EmployeeT WHERE EmployeeID = @EmployeeID",
                                  param: new { ProductID = id }, transaction: Transaction);
        }

        public void Delete(ProductT entity)
        {
            Connection.Execute(
                         @"DELETE FROM ProductT WHERE ProductID = @ProductID
                           DELETE FROM CompanyT_EmployeeT WHERE EmployeeID = @EmployeeID",
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

        public void Update(ProductT entity)
        {
            Connection.Execute(
              @"UPDATE ProductT 
                SET (ProductName = @ProductName,
                        ProductType, = @ProductType,
                        Price = @Price,
                        Unit  = @Unit)
                WHERE ProductID = @ProductID",
              param: new
              {
                  ProductName = entity.ProductName,
                  ProductType = entity.ProductType,
                  Price = entity.Price,
                  Unit = entity.Unit
              }, transaction: Transaction);
        }
    }
}
