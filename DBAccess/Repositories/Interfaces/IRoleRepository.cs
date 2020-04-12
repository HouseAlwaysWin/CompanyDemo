﻿using CompanyDemo.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace DBAccess.Repositories.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role FindByName(string roleName);
        Task<Role> FindByNameAsync(string roleName);
        Task<Role> FindByNameAsync(CancellationToken cancellationToken, string roleName);
    }
}