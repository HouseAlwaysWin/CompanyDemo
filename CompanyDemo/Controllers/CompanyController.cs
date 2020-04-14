using CompanyDemo.Controllers.Base;
using CompanyDemo.Domain.DTOs;
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

        public ActionResult GetCompanyListByID(int page, int? searchText, bool isDesc = false, string sortBy = "CompanyID")
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<CompanyModel>>>(
                    $"https://localhost:44319/api/Company/GetCompanyListByID?current={page}&itemsPerPages=10&&searchText={searchText}&isDesc={isDesc}&&sortBy={sortBy}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("GetCompanyList occured error"));
            }
        }

        [HttpGet]
        [Authorize(Roles = "ShowCompanyList")]
        public ActionResult GetCompanyListByName(int page, string searchText, bool isDesc = false, string sortBy = "CompanyID")
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<CompanyModel>>>(
                    $"https://localhost:44319/api/Company/GetCompanyListByName?current={page}&itemsPerPages=10&&searchText={searchText}&isDesc={isDesc}&&sortBy={sortBy}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("GetCompanyList occured error"));
            }
        }


        [HttpGet]
        public ActionResult GetCompanyByName(string name)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<CompanyModel>>(
                    $"https://localhost:44319/api/Company/GetCompanyByName?name={name}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("GetCompanyList occured error"));
            }
        }


        [HttpGet]
        public ActionResult GetCompanyByID(string id)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<CompanyModel>>>(
                    $"https://localhost:44319/api/Company?id={id}");
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
            var data = RequestHelper.MakePostWebRequest<CompanyModel, Jsend<CompanyModel>>(
                            $"https://localhost:44319/api/Company", new CompanyModel { CompanyID = id }, "DELETE");
            return Jsend(data);
        }


        public ActionResult Index()
        {
            return View();
        }
    }
}