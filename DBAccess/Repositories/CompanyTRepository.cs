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

        public IEnumerable<CompanyT> All()
        {
            return Connection.Query<CompanyT>("SELECT * FROM CompanyT", transaction: Transaction).ToList();
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
                        CreateDate = GETUTCDATE(),
                        EditedDate = @EditedDate
                    WHERE CompanyID = @CompanyID",
               param: new
               {
                   CompanyName = entity.CompanyName,
                   ComapnyCode = entity.CompanyCode,
                   TaxID = entity.TaxID,
                   Phone = entity.Phone,
                   Address = entity.Address,
                   WebsiteURL = entity.WebsiteURL,
                   Owner = entity.Owner,
                   EditedDate = entity.EditedDate
               }, transaction: Transaction);
        }
    }
}
