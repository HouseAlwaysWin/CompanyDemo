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
        EntityWithTotalCount<CompanyT> FindAllByPagination(int currentPage, int itemsPerPages);
        void Delete(int id);
        void Delete(CompanyT entity);
        CompanyT FindByID(int id);
        CompanyT FindByName(string name);
        void Update(CompanyT entity);
    }
}
