using DBAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBAccess.Repositories.Interfaces
{
    public interface IProductTRepository
    {
        void Add(ProductT entity);
        IEnumerable<ProductT> All();
        void Delete(int id);
        void Delete(ProductT entity);
        ProductT FindByID(int id);
        ProductT FindByName(string name);
        void Update(ProductT entity);

    }
}
