﻿using CompanyApiService.Models;
using CompanyApiService.Models.Validators;
using CompanyApiService.Services.Interfaces;
using DBAccess;
using DBAccess.Entities;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

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
            if (data == null) return JsendResult.Error("Employee data can't be null");

            var validator = new EmployeeValidator();
            ValidationResult validateResult = validator.Validate(data);
            if (validateResult.IsValid)
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
            List<ValidationFailure> failures = validateResult.Errors.ToList();

            return JsendResult<List<ValidationFailure>>.Fail(failures);
        }

        public Jsend DeleteEmployee(EmployeeModel data)
        {
            if (data == null) return JsendResult.Error("Employee data can't be null");
            try
            {
                _uow.EmployeeTRepository.Delete(new EmployeeT
                {
                    EmployeeID = data.EmployeeID
                });
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult.Error("DeleteEmployee() Delete data occured error.");
            }
            return JsendResult.Success();
        }

        public Jsend DeleteEmployee(int id)
        {
            try
            {
                _uow.EmployeeTRepository.Delete(new EmployeeT
                {
                    EmployeeID = id
                });
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult.Error("DeleteEmployee() Delete data occured error.");
            }
            return JsendResult.Success();
        }

        public Jsend FindEmployeeByID(int id)
        {
            EmployeeT result = null;

            try
            {
                result = _uow.EmployeeTRepository.FindByID(id);
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult.Error("");
            }
            return JsendResult<EmployeeT>.Success(result);
        }

        public Jsend FindEmployeeByName(string name)
        {
            EmployeeT result = null;

            try
            {
                result = _uow.EmployeeTRepository.FindByName(name);
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult.Error("FindEmployeeByName() occured error");
            }
            return JsendResult<EmployeeT>.Success(result);
        }

        public Jsend InsertUpdateEmployee(EmployeeModel data)
        {
            if (data == null) return JsendResult.Error("Employee data can't be null");
            var validator = new EmployeeValidator();
            ValidationResult validateResult = validator.Validate(data);

            if (validateResult.IsValid)
            {
                try
                {
                    var result = _uow.EmployeeTRepository.FindByID(data.EmployeeID);
                    if (result == null)
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
                    }

                    _uow.EmployeeTRepository.Update(new EmployeeT
                    {
                        EmployeeName = data.EmployeeName,
                        Email = data.Email,
                        BrithdayDate = data.BrithdayDate,
                        SignInDate = data.SignInDate,
                        ResignedDate = data.ResignedDate,
                        IsResigned = data.IsResigned,
                        Salary = data.Salary,
                    });
                    _uow.Commit();
                }
                catch (SqlException ex)
                {
                    _logger.Error(ex);
                    return JsendResult.Error("InsertUpdateEmployee() InsertUpdate data occured error");
                }
                return JsendResult.Success();
            }

            List<ValidationFailure> failures = validateResult.Errors.ToList();

            return JsendResult<List<ValidationFailure>>.Fail(failures);
        }

        public Jsend UpdateEmployee(EmployeeModel data)
        {
            if (data == null) return JsendResult.Error("Employee data can't be null");
            var validator = new EmployeeValidator();
            ValidationResult validateResult = validator.Validate(data);
            if (validateResult.IsValid)
            {
                try
                {
                    _uow.EmployeeTRepository.Update(new EmployeeT
                    {
                        EmployeeName = data.EmployeeName,
                        Email = data.Email,
                        BrithdayDate = data.BrithdayDate,
                        SignInDate = data.SignInDate,
                        ResignedDate = data.ResignedDate,
                        IsResigned = data.IsResigned,
                        Salary = data.Salary,
                    });
                    _uow.Commit();
                }
                catch (SqlException ex)
                {
                    _logger.Error(ex);
                    return JsendResult.Error("InsertUpdateEmployee() InsertUpdate data occured error");
                }
                return JsendResult.Success();
            }

            List<ValidationFailure> failures = validateResult.Errors.ToList();

            return JsendResult<List<ValidationFailure>>.Fail(failures);
        }
    }
}