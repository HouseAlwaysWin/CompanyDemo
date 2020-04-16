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
        //OneToManyMap<ProductT> FindAll(int currentPage, int itemsPerPage, string searchText, bool isDesc = false);
        OneToManyMap<ProductT, CompanyT> FindAllByCompanyID(int companyID, int currentPage, int itemsPerPages, bool isDesc = false);
        OneToManyMap<ProductT, CompanyT> FindAllByProductID(int currentPage, int itemsPerPages, int searchID, bool isDesc = false);
        OneToManyMap<ProductTAndCompanyT> FindAllByCompanyName(string searchText, int currentPage, int itemsPerPage, bool isDesc = false);
        OneToManyMap<ProductTAndCompanyT> FindAllByProductType(string searchText, int currentPage, int itemsPerPage, bool isDesc = false);
        OneToManyMap<ProductTAndCompanyT> FindAllByProductPrice(int? searchText, int currentPage, int itemsPerPage, bool isDesc = false);
        ProductT FindByID(int id);
        ProductT FindByName(string name);
        void Update(ProductT entity);
    }
}