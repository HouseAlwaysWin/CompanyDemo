using DBAccess.Repositories.Interfaces;
using System;

namespace DBAccess
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyTRepository CompanyTRepository { get; }
        IEmployeeTRepository EmployeeTRepository { get; }
        IProductTRepository ProductTRepository { get; }
     
        void Commit();

    }
}
