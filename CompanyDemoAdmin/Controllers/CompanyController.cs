using CompanyDemo.Domain.DTOs;
using CompanyDemoAdmin.Controllers.Base;
using CompanyDemoAdmin.Helpers;
using CompanyDemoAdmin.Models;
using CompanyDemoAdmin.Models.Validators;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CompanyDemoAdmin.Controllers
{
    [Authorize]
    public class CompanyController : BaseController
    {

        private string apiDomain = ConfigurationManager.AppSettings.Get("ApiDomain");

        [HttpGet]

        public ActionResult GetCompanyListByID(int page, int itemsPerPage, int? searchText, bool isDesc = false)
        {
            try
            {
                var url = $"{apiDomain}/api/Company/GetCompanyListByID?current={page}&itemsPerPage={itemsPerPage}&&searchText={searchText}&isDesc={isDesc}";
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<CompanyModel>>>(
                    $"{apiDomain}/api/Company/GetCompanyListByID?current={page}&itemsPerPage={itemsPerPage}&&searchText={searchText}&isDesc={isDesc}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("取得公司發生錯誤"));
            }
        }

        [HttpGet]
        public ActionResult GetCompanyListByName(int page, int itemsPerPage, string searchText, bool isDesc = false)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<CompanyModel>>>(
                    $"{apiDomain}/api/Company/GetCompanyListByName?current={page}&itemsPerPage={itemsPerPage}0&&searchText={searchText}&isDesc={isDesc}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("取得公司發生錯誤"));
            }
        }


        [HttpGet]
        public ActionResult GetCompanyByName(string name)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<CompanyModel>>(
                    $"{apiDomain}/api/Company/GetCompanyByName?name={name}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("取得公司發生錯誤"));
            }
        }


        [HttpGet]
        public ActionResult GetCompanyByID(string id)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<CompanyModel>>>(
                    $"{apiDomain}/api/Company?id={id}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("取得公司發生錯誤"));
            }
        }


        [HttpGet]
        public ActionResult FindComapnyByName(string name)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<CompanyModel>>>(
                    $"{apiDomain}/api/Company/FindComapnyByName?name={name}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("取得公司發生錯誤"));
            }
        }

        [HttpGet]
        public ActionResult FindComapnyByCompanyCode(string code)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<CompanyModel>>>(
                    $"{apiDomain}/api/Company/FindComapnyByCompanyCode?code={code}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("取得公司發生錯誤"));
            }
        }

        [HttpGet]
        public ActionResult FindComapnyByTaxID(string id)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<CompanyModel>>>(
                    $"{apiDomain}/api/Company/FindComapnyByTaxID?id={id}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("取得公司發生錯誤"));
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
                        $"{apiDomain}/api/Company", data);
                    return Jsend(result);
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex);
                    return Jsend(JsendResult.Error("新增公司發生錯誤"));
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
                    $"{apiDomain}/api/Company", data, "PUT");
                    return Jsend(result);
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex);
                    return Jsend(JsendResult.Error("取得公司發生錯誤"));
                }
            }
            List<ValidationFailure> failures = validateResult.Errors.ToList();
            return Jsend(JsendResult<List<ValidationFailure>>.Fail(failures));
        }

        [HttpPost]
        public ActionResult DeleteCompanyByID(int id)
        {
            try
            {
                var data = RequestHelper.MakePostWebRequest<CompanyModel, Jsend<CompanyModel>>(
                                $"{apiDomain}/api/Company", new CompanyModel { CompanyID = id }, "DELETE");
                return Jsend(data);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("刪除公司發生錯誤"));
            }
        }


        public ActionResult Index()
        {
            return View();
        }
    }
}