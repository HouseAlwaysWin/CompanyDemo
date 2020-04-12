using CompanyDemo.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace DBAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByUserName(string username);
        Task<User> FindByUserNameAsync(string username);
        Task<User> FindByUserNameAsync(CancellationToken cancellationToken, string username);
    }
}
