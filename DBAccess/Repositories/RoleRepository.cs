using CompanyDemo.Domain.Entities;
using DBAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DBAccess.Repositories
{
    internal class RoleRepository : RepositoryBase, IRoleRepository
    {
        public RoleRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public Role FindByName(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<Role> FindByNameAsync(System.Threading.CancellationToken cancellationToken, string roleName)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Role>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Role>> GetAllAsync(System.Threading.CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public List<Role> PageAll(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task<List<Role>> PageAllAsync(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task<List<Role>> PageAllAsync(System.Threading.CancellationToken cancellationToken, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Role FindById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<Role> FindByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<Role> FindByIdAsync(System.Threading.CancellationToken cancellationToken, object id)
        {
            throw new NotImplementedException();
        }

        public void Add(Role entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Role entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Role entity)
        {
            throw new NotImplementedException();
        }
    }
}
