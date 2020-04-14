using System.Collections.Generic;
using CompanyDemo.Domain.DTOs;
using CompanyDemo.Domain.Entities;

namespace DBAccess.Repositories.Interfaces
{
    public interface IProductTRepository
    {
        void Add(ProductT entity);
        IEnumerable<ProductT> All();
        void Delete(int id);
        void Delete(ProductT entity);
        OneToManyMap<ProductT, CompanyT> FindAllByCompanyID(int companyID, int currentPage, int itemsPerPages, bool isDesc = false);
        ProductT FindByID(int id);
        ProductT FindByName(string name);
        void Update(ProductT entity);
    }
}