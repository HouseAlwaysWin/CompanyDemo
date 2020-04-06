using DBAccess.Entities;
using DBAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DBAccess.Repositories
{
    internal class EmployeeTRepository : RepositoryBase, IEmployeeTRepository
    {
        public EmployeeTRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public void Add(EmployeeT entity)
        {

        }

        public IEnumerable<EmployeeT> All()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(EmployeeT entity)
        {
            throw new NotImplementedException();
        }

        public EmployeeT Find(int id)
        {
            throw new NotImplementedException();
        }

        public EmployeeT FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(EmployeeT entity)
        {
            throw new NotImplementedException();
        }
    }
}
