using CompanyApiService.Models;
using CompanyApiService.Models.Validators;
using CompanyApiService.Services.Interfaces;
using DBAccess;
using DBAccess.DTO;
using DBAccess.Entities;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CompanyApiService.Services
{
    public class CompanyService : ICompanyService
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        IUnitOfWork _uow;
        public CompanyService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Jsend<List<ValidationFailure>> AddCompany(CompanyModel data)
        {
            if (data == null) return JsendResult<List<ValidationFailure>>.Error("Company data can't be null");

            var checkNameUnique = _uow.CompanyTRepository.FindByName(data.CompanyName);
            if (checkNameUnique != null) return JsendResult<List<ValidationFailure>>.Error("CompanyName has already had");

            var validator = new CompanyValidator();
            ValidationResult validateResult = validator.Validate(data);
            if (validateResult.IsValid)
            {
                try
                {
                    _uow.CompanyTRepository.Add(
                        new CompanyT
                        {
                            CompanyName = data.CompanyName,
                            CompanyCode = data.CompanyCode,
                            TaxID = data.TaxID,
                            Phone = data.Phone,
                            Owner = data.Owner,
                            Address = data.Address
                        });
                    _uow.Commit();
                }
                catch (SqlException ex)
                {
                    _logger.Error(ex);
                    return JsendResult<List<ValidationFailure>>.Error("Insert data error");
                }

                return JsendResult<List<ValidationFailure>>.Success();
            }
            List<ValidationFailure> failures = validateResult.Errors.ToList();


            return JsendResult<List<ValidationFailure>>.Fail(failures);
        }

        public Jsend DeleteCompany(CompanyModel data)
        {
            if (data == null) return JsendResult.Error("Company data can't be null");
            try
            {
                _uow.CompanyTRepository.Delete(new CompanyT
                {
                    CompanyID = data.CompanyID
                });
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult.Error("Delete data occured error.");
            }
            return JsendResult.Success();
        }

        public Jsend DeleteCompanyByID(int id)
        {
            try
            {
                _uow.CompanyTRepository.Delete(id);
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult.Error("Delete data occured error.");
            }
            return JsendResult.Success();
        }

        public Jsend<CompanyT> FindComapnyByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return JsendResult<CompanyT>.Error("name can't be null");
            CompanyT result = null;
            try
            {
                result = _uow.CompanyTRepository.FindByName(name);
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult<CompanyT>.Error("Queay data occured error");
            }

            return JsendResult<CompanyT>.Success(result);
        }

        public Jsend<CompanyT> FindCompanyByID(int id)
        {
            CompanyT result = null;
            try
            {
                result = _uow.CompanyTRepository.FindByID(id);
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult<CompanyT>.Error("Queay data occured error");
            }

            return JsendResult<CompanyT>.Success(result);
        }


        public Jsend<EntityWithTotalCount<CompanyT>> FindCompanyListByPage(int current, int itemsPerPages)
        {
            EntityWithTotalCount<CompanyT> result = null;
            try
            {
                result = _uow.CompanyTRepository.FindAllByPagination(current, itemsPerPages);
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult<EntityWithTotalCount<CompanyT>>.Error("Queay data occured error");
            }
            return JsendResult<EntityWithTotalCount<CompanyT>>.Success(result);
        }

        public Jsend<List<ValidationFailure>> UpdateCompany(CompanyModel data)
        {
            if (data == null) return JsendResult<List<ValidationFailure>>.Error("Company data can't be null");

            var validator = new CompanyValidator();
            ValidationResult validateResult = validator.Validate(data);
            if (validateResult.IsValid)
            {
                try
                {
                    _uow.CompanyTRepository.Update(
                        new CompanyT
                        {
                            CompanyID = data.CompanyID,
                            CompanyName = data.CompanyName,
                            CompanyCode = data.CompanyCode,
                            TaxID = data.TaxID,
                            Phone = data.Phone,
                            Owner = data.Owner,
                            Address = data.Address
                        });
                    _uow.Commit();
                }
                catch (SqlException ex)
                {
                    _logger.Error(ex);
                    return JsendResult<List<ValidationFailure>>.Error("Queay data occured error");
                }

                return JsendResult<List<ValidationFailure>>.Success();
            }
            List<ValidationFailure> failures = validateResult.Errors.ToList();


            return JsendResult<List<ValidationFailure>>.Fail(failures);
        }


        public Jsend<List<ValidationFailure>> InsertUpdateCompany(CompanyModel data)
        {
            if (data == null) return JsendResult<List<ValidationFailure>>.Error("Company data can't be null");

            var validator = new CompanyValidator();
            ValidationResult validateResult = validator.Validate(data);
            if (validateResult.IsValid)
            {
                try
                {
                    var result = _uow.CompanyTRepository.FindByID(data.CompanyID);
                    if (result == null)
                    {
                        _uow.CompanyTRepository.Add(
                            new CompanyT
                            {
                                CompanyName = data.CompanyName,
                                CompanyCode = data.CompanyCode,
                                TaxID = data.TaxID,
                                Phone = data.Phone,
                                Owner = data.Owner,
                                Address = data.Address
                            });
                    }
                    else
                    {
                        _uow.CompanyTRepository.Update(
                            new CompanyT
                            {
                                CompanyID = data.CompanyID,
                                CompanyName = data.CompanyName,
                                CompanyCode = data.CompanyCode,
                                TaxID = data.TaxID,
                                Phone = data.Phone,
                                Owner = data.Owner,
                                Address = data.Address
                            });
                    }
                    _uow.Commit();
                }
                catch (SqlException ex)
                {
                    _logger.Error(ex);
                    return JsendResult<List<ValidationFailure>>.Error("Queay data occured error");
                }

                return JsendResult<List<ValidationFailure>>.Success();
            }
            List<ValidationFailure> failures = validateResult.Errors.ToList();


            return JsendResult<List<ValidationFailure>>.Fail(failures);
        }
    }
}
