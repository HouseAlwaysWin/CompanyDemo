using DBAccess.DTO;
using DBAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBAccess.Repositories.Interfaces
{
    public interface ICompanyTRepository
    {
        void Add(CompanyT entity);
        IEnumerable<CompanyT> All();
        EntityWithTotalCount<CompanyT> FindAllByID(int currentPage, int itemsPerPages, int? searchText, bool isDesc = false, string sortBy = "CompanyID");
        EntityWithTotalCount<CompanyT> FindAllByName(int currentPage, int itemsPerPages, string searchText, bool isDesc = false, string sortBy = "CompanyID");
        void Delete(int id);
        void Delete(CompanyT entity);
        CompanyT FindByID(int id);
        CompanyT FindByName(string name);
        void Update(CompanyT entity);
    }
}
