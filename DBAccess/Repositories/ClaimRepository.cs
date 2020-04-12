using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CompanyDemo.Domain.Entities;
using Dapper;
using DBAccess.Repositories;
using DBAccess.Repositories.Interfaces;

namespace DBAccess.Repositories
{
    internal class ClaimRepository : RepositoryBase, IClaimRepository
    {
        internal ClaimRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public List<Claim> GetAll()
        {
            return Connection
                .Query<Claim>("SELECT ClaimId, UserId, ClaimType, ClaimValue FROM Claim", transaction: Transaction).ToList();
        }

        public Task<List<Claim>> GetAllAsync()
        {
            var claimProxies = Connection
                .Query<Claim>("SELECT ClaimId, UserId, ClaimType, ClaimValue FROM Claim", transaction: Transaction);
            return Task.FromResult<List<Claim>>(new List<Claim>(claimProxies));
        }

        public List<Claim> PageAll(int skip, int take)
        {
            var claimProxies = Connection
                .Query<Claim>("SELECT ClaimId, UserId, ClaimType, ClaimValue FROM Claim OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY", param: new { Skip = skip, Take = take }, transaction: Transaction);
            return new List<Claim>(claimProxies);
        }

        public Task<List<Claim>> PageAllAsync(int skip, int take)
        {
            var claimProxies = Connection
                .Query<Claim>("SELECT ClaimId, UserId, ClaimType, ClaimValue FROM Claim OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY", param: new { Skip = skip, Take = take }, transaction: Transaction);
            return Task.FromResult<List<Claim>>(new List<Claim>(claimProxies));
        }

        public Claim FindById(object id)
        {
            return Connection
                .Query<Claim>("SELECT ClaimId, UserId, ClaimType, ClaimValue FROM Claim WHERE ClaimId = @ClaimId", param: new { ClaimId = id }, transaction: Transaction)
                .FirstOrDefault();
        }

        public Task<Claim> FindByIdAsync(object id)
        {
            return Task.FromResult<Claim>(Connection
                .Query<Claim>("SELECT ClaimId, UserId, ClaimType, ClaimValue FROM Claim WHERE ClaimId = @ClaimId", param: new { ClaimId = id }, transaction: Transaction)

                .FirstOrDefault());
        }

        public void Add(Claim entity)
        {
            entity.ClaimId = Connection.ExecuteScalar<int>(
                "INSERT INTO Claim(UserId, ClaimType, ClaimValue VALUES(@UserId, @ClaimType, @ClaimValue)",
                param: new { UserId = entity.UserId, ClaimType = entity.ClaimType, ClaimValue = entity.ClaimValue },
                transaction: Transaction);
        }

        public void Update(Claim entity)
        {
            Connection.Execute(
                "UPDATE Claim SET UserId = @UserId, ClaimType = @ClaimType, ClaimValue = @ClaimValue WHERE ClaimId = @ClaimId",
                param: new { ClaimId = entity.ClaimId, UserId = entity.UserId, ClaimType = entity.ClaimType, ClaimValue = entity.ClaimValue },
                transaction: Transaction);
        }

        public void Remove(Claim entity)
        {
            Connection.Execute(
                "DELETE FROM Claim WHERE ClaimId = @ClaimId",
                param: new { ClaimId = entity.ClaimId },
                transaction: Transaction);
        }
    }
}
