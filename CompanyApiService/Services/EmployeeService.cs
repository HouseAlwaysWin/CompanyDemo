using CompanyApiService.Models;
using DBAccess;
using DBAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CompanyApiService.Services
{
    public class EmployeeService : IEmployeeService
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        IUnitOfWork _uow;
        public EmployeeService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public Jsend AddEmployee(EmployeeModel data)
        {
            try
            {
                _uow.EmployeeTRepository.Add(new EmployeeT
                {
                    EmployeeName = data.EmployeeName,
                    Email = data.Email,
                    BrithdayDate = data.BrithdayDate,
                    SignInDate = data.SignInDate,
                    ResignedDate = data.ResignedDate,
                    IsResigned = data.IsResigned,
                    CompanyID = data.CompanyID
                });
                _uow.Commit();
                return JsendResult.Success();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult.Error("Insert Employee occured error");
            }
        }

        public Jsend DeleteEmployee(EmployeeModel data)
        {
            throw new NotImplementedException();
        }

        public Jsend DeleteEmployeeByID(int id)
        {
            throw new NotImplementedException();
        }

        public Jsend FindEmployeeByID(int Id)
        {
            throw new NotImplementedException();
        }

        public Jsend FindEmployeeByName(string name)
        {
            throw new NotImplementedException();
        }

        public Jsend InsertUpdateEmployee(EmployeeModel data)
        {
            throw new NotImplementedException();
        }

        public Jsend UpdateEmployee(EmployeeModel data)
        {
            throw new NotImplementedException();
        }
    }
}