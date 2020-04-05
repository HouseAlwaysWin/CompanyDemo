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
        EmployeeT Find(int id);
        EmployeeT FindByName(string name);
        void Update(EmployeeT entity);

    }
}
