using DBAccess.Entities;
using DBAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
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
            throw new NotImplementedException();
        }

        public IEnumerable<ProductT> All()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(ProductT entity)
        {
            throw new NotImplementedException();
        }

        public ProductT Find(int id)
        {
            throw new NotImplementedException();
        }

        public ProductT FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductT entity)
        {
            throw new NotImplementedException();
        }
    }
}
