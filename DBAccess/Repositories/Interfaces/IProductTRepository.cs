using CompanyDemo.Domain.Entities;
using System.Collections.Generic;

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
