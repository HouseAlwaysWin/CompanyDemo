using CompanyDemo.Domain.DTOs;
using CompanyDemo.Domain.Entities;
using System.Collections.Generic;

namespace DBAccess.Repositories.Interfaces
{
    public interface IEmployeeTRepository
    {
        void Add(EmployeeT entity);
        IEnumerable<EmployeeT> All();
        void Delete(int id);
        void Delete(EmployeeT entity);
        EmployeeT FindByID(int id);
        OneToManyMap<EmployeeT, CompanyT> FindAllByCompanyID(int companyID, int currentPage, int itemsPerPages, bool isDesc = false);
        OneToManyMap<EmployeeT> FindAllByName(int currentPage, int itemsPerPages, string searchText, bool isDesc = false, string sortBy = "EmployeeID");
        EmployeeT FindByName(string name);
        void Update(EmployeeT entity);

    }
}
