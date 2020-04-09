using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Threading;
using DBAccess.Repositories.Interfaces;
using DBAccess.Entities;

namespace DBAccess.Repositories
{
    internal class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public User FindByUserName(string username)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByUserNameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public IList<User> FindByRole(Role role)
        {
            throw new NotImplementedException();
        }

        public Task<IList<User>> FindByRoleAsync(Role role)
        {
            return Task.FromResult<IList<User>>(FindByRole(role));
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public List<User> PageAll(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> PageAllAsync(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public User FindById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(User entity)
        {
            throw new NotImplementedException();
        }




        //internal UserProxy FindProxyById(object id)
        //{
        //    throw new NotImplementedException();
        //}

        //internal UserProxy GetUserProxy(User user)
        //{
        //    return new UserProxy(UnitOfWork)
        //    {
        //        PasswordHash = user.PasswordHash,
        //        SecurityStamp = user.SecurityStamp,
        //        UserId = user.UserId,
        //        UserName = user.UserName
        //    };
        //}

        //internal IList<UserProxy> GetUserProxiesByRole(Role role)
        //{
        //    return UnitOfWork.Connection.Query<User>(
        //        "SELECT u.PasswordHash, u.SecurityStamp, u.UserId, u.UserName FROM [User] u INNER JOIN UserRole ur ON u.UserId = ur.UserId WHERE ur.RoleId = @RoleId",
        //        param: new { RoleId = role.RoleId },
        //        transaction: UnitOfWork.Transaction
        //    ).Select(x => GetUserProxy(x)).ToList();
        //}

        public Task<User> FindByUserNameAsync(CancellationToken cancellationToken, string username)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> PageAllAsync(CancellationToken cancellationToken, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(CancellationToken cancellationToken, object id)
        {
            throw new NotImplementedException();
        }
    }
}
