using CompanyApiService.Models;
using CompanyApiService.Models.Validators;
using CompanyApiService.Services.Interfaces;
using CompanyDemo.Domain.DTOs;
using CompanyDemo.Domain.Entities;
using DBAccess;
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
        public Jsend<List<ValidationFailure>> AddEmployee(EmployeeModel data)
        {
            if (data == null) return JsendResult<List<ValidationFailure>>.Error("Employee data can't be null");

            var validator = new EmployeeValidator();
            ValidationResult validateResult = validator.Validate(data);
            if (validateResult.IsValid)
            {
                try
                {
                    _uow.EmployeeTRepository.Add(new EmployeeT
                    {
                        EmployeeName = data.EmployeeName,
                        EmployeePosition = data.EmployeePosition,
                        EmployeePhone = data.EmployeePhone,
                        Email = data.Email,
                        BirthdayDate = data.BirthdayDate,
                        SignInDate = data.SignInDate,
                        ResignedDate = data.ResignedDate,
                        IsResigned = data.IsResigned,
                        CompanyID = data.CompanyID
                    });
                    _uow.Commit();
                    return JsendResult<List<ValidationFailure>>.Success();
                }
                catch (SqlException ex)
                {
                    _logger.Error(ex);
                    return JsendResult<List<ValidationFailure>>.Error("Insert Employee occured error");
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

        public Jsend<EmployeeT> FindEmployeeByID(int id)
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
                return JsendResult<EmployeeT>.Error("");
            }
            return JsendResult<EmployeeT>.Success(result);
        }

        public Jsend<EmployeeT> FindEmployeeByName(string name)
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
                return JsendResult<EmployeeT>.Error("FindEmployeeByName() occured error");
            }
            return JsendResult<EmployeeT>.Success(result);
        }


        public Jsend<OneToManyMap<EmployeeT, CompanyT>> FindCompanyListByID(int id, int current, int itemsPerPages, bool isDesc)
        {
            OneToManyMap<EmployeeT, CompanyT> result = null;
            try
            {
                result = _uow.EmployeeTRepository.FindAllByCompanyID(id, current, itemsPerPages, isDesc);
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult<OneToManyMap<EmployeeT, CompanyT>>.Error("Queay data occured error");
            }
            return JsendResult<OneToManyMap<EmployeeT, CompanyT>>.Success(result);
        }


        public Jsend<OneToManyMap<EmployeeTAndCompanyT>> FindAllByEmployeeName(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            OneToManyMap<EmployeeTAndCompanyT> result = null;
            try
            {
                result = _uow.EmployeeTRepository.FindAllByEmployeeName(companyID, searchText, currentPage, itemsPerPage, isDesc);
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult<OneToManyMap<EmployeeTAndCompanyT>>.Error("Queay data occured error");
            }
            return JsendResult<OneToManyMap<EmployeeTAndCompanyT>>.Success(result);
        }

        public Jsend<OneToManyMap<EmployeeTAndCompanyT>> FindAllByEmployeeID(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            OneToManyMap<EmployeeTAndCompanyT> result = null;
            try
            {
                result = _uow.EmployeeTRepository.FindAllByEmployeeID(companyID, searchText, currentPage, itemsPerPage, isDesc);
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult<OneToManyMap<EmployeeTAndCompanyT>>.Error("Queay data occured error");
            }
            return JsendResult<OneToManyMap<EmployeeTAndCompanyT>>.Success(result);
        }

        public Jsend<OneToManyMap<EmployeeTAndCompanyT>> FindAllByEmployeePhone(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            OneToManyMap<EmployeeTAndCompanyT> result = null;
            try
            {
                result = _uow.EmployeeTRepository.FindAllByEmployeePhone(companyID, searchText, currentPage, itemsPerPage, isDesc);
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult<OneToManyMap<EmployeeTAndCompanyT>>.Error("Queay data occured error");
            }
            return JsendResult<OneToManyMap<EmployeeTAndCompanyT>>.Success(result);
        }


        public Jsend<OneToManyMap<EmployeeTAndCompanyT>> FindAllByEmployeePosition(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            OneToManyMap<EmployeeTAndCompanyT> result = null;
            try
            {
                result = _uow.EmployeeTRepository.FindAllByEmployeePosition(companyID, searchText, currentPage, itemsPerPage, isDesc);
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult<OneToManyMap<EmployeeTAndCompanyT>>.Error("Queay data occured error");
            }
            return JsendResult<OneToManyMap<EmployeeTAndCompanyT>>.Success(result);
        }

        public Jsend<OneToManyMap<EmployeeTAndCompanyT>> FindAllByEmployeeBirthday(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            OneToManyMap<EmployeeTAndCompanyT> result = null;
            try
            {
                result = _uow.EmployeeTRepository.FindAllByEmployeeBirthday(companyID, searchText, currentPage, itemsPerPage, isDesc);
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult<OneToManyMap<EmployeeTAndCompanyT>>.Error("Queay data occured error");
            }
            return JsendResult<OneToManyMap<EmployeeTAndCompanyT>>.Success(result);
        }

        public Jsend<List<ValidationFailure>> InsertUpdateEmployee(EmployeeModel data)
        {
            if (data == null) return JsendResult<List<ValidationFailure>>.Error("Employee data can't be null");
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
                            EmployeePosition = data.EmployeePosition,
                            EmployeePhone = data.EmployeePhone,
                            Email = data.Email,
                            BirthdayDate = data.BirthdayDate,
                            SignInDate = data.SignInDate,
                            ResignedDate = data.ResignedDate,
                            IsResigned = data.IsResigned,
                            CompanyID = data.CompanyID
                        });
                    }
                    else
                    {
                        _uow.EmployeeTRepository.Update(new EmployeeT
                        {
                            EmployeeID = data.EmployeeID,
                            EmployeeName = data.EmployeeName,
                            EmployeePosition = data.EmployeePosition,
                            EmployeePhone = data.EmployeePhone,
                            Email = data.Email,
                            BirthdayDate = data.BirthdayDate,
                            SignInDate = data.SignInDate,
                            ResignedDate = data.ResignedDate,
                            IsResigned = data.IsResigned,
                            Salary = data.Salary,
                        });
                    }
                    _uow.Commit();
                }
                catch (SqlException ex)
                {
                    _logger.Error(ex);
                    return JsendResult<List<ValidationFailure>>.Error("InsertUpdateEmployee() InsertUpdate data occured error");
                }
                return JsendResult<List<ValidationFailure>>.Success();
            }

            List<ValidationFailure> failures = validateResult.Errors.ToList();

            return JsendResult<List<ValidationFailure>>.Fail(failures);
        }

        public Jsend<List<ValidationFailure>> UpdateEmployee(EmployeeModel data)
        {
            if (data == null) return JsendResult<List<ValidationFailure>>.Error("Employee data can't be null");
            var validator = new EmployeeValidator();
            ValidationResult validateResult = validator.Validate(data);
            if (validateResult.IsValid)
            {
                try
                {
                    _uow.EmployeeTRepository.Update(new EmployeeT
                    {
                        EmployeeID = data.EmployeeID,
                        EmployeeName = data.EmployeeName,
                        EmployeePhone = data.EmployeePhone,
                        EmployeePosition = data.EmployeePosition,
                        Email = data.Email,
                        BirthdayDate = data.BirthdayDate,
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
                    return JsendResult<List<ValidationFailure>>.Error("InsertUpdateEmployee() InsertUpdate data occured error");
                }
                return JsendResult<List<ValidationFailure>>.Success();
            }

            List<ValidationFailure> failures = validateResult.Errors.ToList();

            return JsendResult<List<ValidationFailure>>.Fail(failures);
        }
    }
}