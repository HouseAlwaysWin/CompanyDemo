using CompanyApiService.Models;
using CompanyApiService.Models.Validators;
using DBAccess;
using DBAccess.Entities;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace CompanyApiService.Services
{
    public class CompanyService : ICompanyService
    {
        IUnitOfWork _uow;
        public CompanyService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Jsend AddCompany(CompanyModel data)
        {
            if (data == null) return JsendResult.Error("Company data can't be null");

            var validator = new CompanyValidator();
            ValidationResult validateResult = validator.Validate(data);
            if (validateResult.IsValid)
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

                return JsendResult.Success();
            }
            List<ValidationFailure> failures = validateResult.Errors.ToList();


            return JsendResult<List<ValidationFailure>>.Fail(failures);
        }
    }
}
