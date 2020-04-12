using CompanyDemo.Domain.DTOs;
using CompanyDemo.Domain.Entities;
using System.Collections.Generic;

namespace DBAccess.Repositories.Interfaces
{
    public interface ICompanyTRepository
    {
        void Add(CompanyT entity);
        IEnumerable<CompanyT> All();
        OneToManyMap<CompanyT> FindAllByID(int currentPage, int itemsPerPages, int? searchText, bool isDesc = false, string sortBy = "CompanyID");
        OneToManyMap<CompanyT> FindAllByName(int currentPage, int itemsPerPages, string searchText, bool isDesc = false, string sortBy = "CompanyID");
        void Delete(int id);
        void Delete(CompanyT entity);
        CompanyT FindByID(int id);
        CompanyT FindByName(string name);
        void Update(CompanyT entity);
    }
}
