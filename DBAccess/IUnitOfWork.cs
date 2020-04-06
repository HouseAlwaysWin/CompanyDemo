﻿using DBAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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