using CompanyDemo.Domain.Entities;
using DBAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DBAccess.Repositories
{
    internal class ExternalLoginRepository : RepositoryBase, IExternalLoginRepository
    {
        public ExternalLoginRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public ExternalLogin GetByProviderAndKey(string loginProvider, string providerKey)
        {
            throw new NotImplementedException();
        }

        public Task<ExternalLogin> GetByProviderAndKeyAsync(string loginProvider, string providerKey)
        {
            throw new NotImplementedException();
        }

        public Task<ExternalLogin> GetByProviderAndKeyAsync(System.Threading.CancellationToken cancellationToken, string loginProvider, string providerKey)
        {
            throw new NotImplementedException();
        }

        public List<ExternalLogin> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<ExternalLogin>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ExternalLogin>> GetAllAsync(System.Threading.CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public List<ExternalLogin> PageAll(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task<List<ExternalLogin>> PageAllAsync(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task<List<ExternalLogin>> PageAllAsync(System.Threading.CancellationToken cancellationToken, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public ExternalLogin FindById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<ExternalLogin> FindByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<ExternalLogin> FindByIdAsync(System.Threading.CancellationToken cancellationToken, object id)
        {
            throw new NotImplementedException();
        }

        public void Add(ExternalLogin entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ExternalLogin entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ExternalLogin entity)
        {
            throw new NotImplementedException();
        }
    }
}
