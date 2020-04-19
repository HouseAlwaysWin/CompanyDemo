using System.Collections.Generic;
using CompanyDemo.Domain.DTOs;
using CompanyDemo.Domain.Entities;

namespace DBAccess.Repositories.Interfaces
{
    public interface ICompanyTRepository
    {
        void Add(CompanyT entity);
        IEnumerable<CompanyT> All();
        void Delete(CompanyT entity);
        void Delete(int id);
        OneToManyMap<CompanyT> FindAllByID(int currentPage, int itemsPerPage, string searchText = null, bool isDesc = false);
        OneToManyMap<CompanyT> FindAllByName(int currentPage, int itemsPerPage, string searchText, bool isDesc = false);
        CompanyT FindByID(int id);
        CompanyT FindByName(string name);
        CompanyT FindByCompanyCode(string code);
        CompanyT FindByTaxID(string id);
        void Update(CompanyT entity);
    }
}