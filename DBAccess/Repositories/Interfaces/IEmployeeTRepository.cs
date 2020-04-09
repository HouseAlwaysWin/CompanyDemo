using DBAccess.DTO;
using DBAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBAccess.Repositories.Interfaces
{
    public interface IEmployeeTRepository
    {
        void Add(EmployeeT entity);
        IEnumerable<EmployeeT> All();
        void Delete(int id);
        void Delete(EmployeeT entity);
        EmployeeT FindByID(int id);
        EntityWithTotalCount<EmployeeT> FindAllByID(int currentPage, int itemsPerPages, int? searchText, bool isDesc = false, string sortBy = "EmployeeID");
        EntityWithTotalCount<EmployeeT> FindAllByName(int currentPage, int itemsPerPages, string searchText, bool isDesc = false, string sortBy = "EmployeeID");
        EmployeeT FindByName(string name);
        void Update(EmployeeT entity);

    }
}
