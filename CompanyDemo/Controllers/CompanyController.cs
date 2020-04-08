using CompanyDemo.Controllers.Base;
using CompanyDemo.Helpers;
using CompanyDemo.Models;
using CompanyDemo.Models.Validators;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CompanyDemo.Controllers
{
    public class CompanyController : BaseController
    {
        [HttpGet]
        public ActionResult GetCompanyList(int page)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<CompanyModel, Jsend<GenericPaginationModel<CompanyModel>>>(
                    $"https://localhost:44319/api/Company/GetCompanyAllByPage?current={page}&itemsPerPages=10");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("GetCompanyList occured error"));
            }
        }


        [HttpPost]
        public ActionResult AddCompany(CompanyModel data)
        {
            if (data == null) return Jsend(JsendResult<List<ValidationFailure>>.Error("Company data can't be null"));
            var validator = new CompanyValidator();
            ValidationResult validateResult = validator.Validate(data);

            if (validateResult.IsValid)
            {
                try
                {
                    var result = RequestHelper.MakePostWebRequest<CompanyModel, Jsend<CompanyModel>>(
                        $"https://localhost:44319/api/Company", data);
                    return Jsend(result);
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex);
                    return Jsend(JsendResult.Error("Add Company occured error"));
                }
            }
            List<ValidationFailure> failures = validateResult.Errors.ToList();
            return Jsend(JsendResult<List<ValidationFailure>>.Fail(failures));
        }


        [HttpPost]
        public ActionResult UpdateCompany(CompanyModel data)
        {
            if (data == null) return Jsend(JsendResult<List<ValidationFailure>>.Error("Company data can't be null"));
            var validator = new CompanyValidator();
            ValidationResult validateResult = validator.Validate(data);

            if (validateResult.IsValid)
            {
                try
                {
                    var result = RequestHelper.MakePostWebRequest<CompanyModel, Jsend<CompanyModel>>(
                    $"https://localhost:44319/api/Company", data, "PUT");
                    return Jsend(result);
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex);
                    return Jsend(JsendResult.Error("Add Company occured error"));
                }
            }
            List<ValidationFailure> failures = validateResult.Errors.ToList();
            return Jsend(JsendResult<List<ValidationFailure>>.Fail(failures));
        }

        [HttpPost]
        public ActionResult DeleteCompanyByID(int id)
        {
            var data = RequestHelper.MakePostWebRequest<int, Jsend<CompanyModel>>(
                            $"https://localhost:44319/api/Company", id, "DELETE");
            return Jsend(data);
        }


        public ActionResult Index()
        {
            return View();
        }
    }
}