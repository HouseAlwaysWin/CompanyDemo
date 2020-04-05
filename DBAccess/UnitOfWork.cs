using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DBAccess.Repositories.Interfaces;
using System.Data.SqlClient;
using DBAccess.Repositories;

namespace DBAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private ICompanyTRepository _companyTRepository;
        private IEmployeeTRepository _employeeTRepository;
        private IProductTRepository _productTRepository;
        private bool _disposed;

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            try
            {
                _connection.Open();
            }
            catch (Exception ex)
            {

            }
            _transaction = _connection.BeginTransaction();
        }

        public ICompanyTRepository CompanyTRepository
        {
            get { return _companyTRepository ?? (_companyTRepository = new CompanyTRepository(_transaction)); }
        }

        public IEmployeeTRepository EmployeeTRepository
        {
            get { return _employeeTRepository ?? (_employeeTRepository = new EmployeeTRepository(_transaction)); }
        }

        public IProductTRepository ProductTRepository
        {
            get { return _productTRepository ?? (_productTRepository = new ProductTRepository(_transaction)); }
        }
        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _companyTRepository = null;
            _employeeTRepository = null;
            _productTRepository = null;

        }


        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }

    }
}
