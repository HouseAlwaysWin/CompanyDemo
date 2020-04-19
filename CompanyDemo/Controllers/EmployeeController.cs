using CompanyDemo.Controllers.Base;
using CompanyDemo.Domain.DTOs;
using CompanyDemo.Helpers;
using CompanyDemo.Models;
using CompanyDemo.Models.Validators;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDemo.Controllers
{
    [Authorize]
    public class EmployeeController : BaseController
    {
        private string apiDomain = ConfigurationManager.AppSettings.Get("ApiDomain");

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetEmployeeListByID(int page, int? searchText, bool isDesc = false, string sortBy = "EmployeeID")
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<EmployeeModel>>>(
                    $"{apiDomain}/api/Employee/GetEmployeeListByID?current={page}&itemsPerPages=10&&searchText={searchText}&isDesc={isDesc}&&sortBy={sortBy}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("GetEmployeeList occured error"));
            }
        }

        [HttpGet]
        public ActionResult GetEmployeeListByName(int page, string searchText, bool isDesc = false, string sortBy = "EmployeeID")
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<EmployeeModel>>>(
                    $"{apiDomain}/api/Employee/GetEmployeeListByName?current={page}&itemsPerPages=10&&searchText={searchText}&isDesc={isDesc}&&sortBy={sortBy}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("GetEmployeeList occured error"));
            }
        }


        [HttpGet]
        public ActionResult GetEmployeeByName(string name)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<EmployeeModel>>(
                    $"{apiDomain}/api/Employee/GetEmployeeByName?name={name}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("GetEmployeeList occured error"));
            }
        }


        [HttpGet]
        public ActionResult GetEmployeeByID(string id)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<EmployeeModel>>>(
                    $"{apiDomain}/api/Employee/?id={id}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("GetEmployeeList occured error"));
            }
        }


        [HttpGet]
        public ActionResult GetListByCompanyID(int id, int currentPage, bool isDesc)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<EmployeeModel, CompanyModel>>>(
                    $"{apiDomain}/api/Employee/GetListByCompanyID?id={id}&currentPage={currentPage}&itemsPerPage=10&isDesc={isDesc}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("GetListByCompanyID occured error"));
            }
        }


        [HttpGet]
        public ActionResult FindAllByEmployeeName(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<EmployeeModel, CompanyModel>>>(
                    $"{apiDomain}/api/Employee/FindAllByEmployeeName?companyID={companyID}&currentPage={currentPage}&searchText={searchText}&itemsPerPage={itemsPerPage}&isDesc={isDesc}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("FindAllByEmployeeName occured error"));
            }
        }

        [HttpGet]
        public ActionResult FindAllByEmployeeID(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<EmployeeModel, CompanyModel>>>(
                    $"{apiDomain}/api/Employee/FindAllByEmployeeID?companyID={companyID}&currentPage={currentPage}&searchText={searchText}&itemsPerPage={itemsPerPage}&isDesc={isDesc}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("FindAllByEmployeeID occured error"));
            }
        }

        [HttpGet]
        public ActionResult FindAllByEmployeePhone(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<EmployeeModel, CompanyModel>>>(
                    $"{apiDomain}/api/Employee/FindAllByEmployeePhone?companyID={companyID}&currentPage={currentPage}&searchText={searchText}&itemsPerPage={itemsPerPage}&isDesc={isDesc}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("FindAllByEmployeePhone occured error"));
            }
        }

        [HttpGet]
        public ActionResult FindAllByEmployeePosition(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<EmployeeModel, CompanyModel>>>(
                    $"{apiDomain}/api/Employee/FindAllByEmployeePosition?companyID={companyID}&currentPage={currentPage}&searchText={searchText}&itemsPerPage={itemsPerPage}&isDesc={isDesc}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("FindAllByEmployeePosition occured error"));
            }
        }

        [HttpGet]
        public ActionResult FindAllByEmployeeBirthday(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<EmployeeModel, CompanyModel>>>(
                    $"{apiDomain}/api/Employee/FindAllByEmployeeBirthday?companyID={companyID}&currentPage={currentPage}&searchText={searchText}&itemsPerPage={itemsPerPage}&isDesc={isDesc}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("FindAllByEmployeeBirthday occured error"));
            }
        }


        [HttpPost]
        public ActionResult AddEmployee(EmployeeModel data)
        {
            if (data == null) return Jsend(JsendResult<List<ValidationFailure>>.Error("Employee data can't be null"));
            var validator = new EmployeeValidator();
            ValidationResult validateResult = validator.Validate(data);

            if (validateResult.IsValid)
            {
                try
                {
                    var result = RequestHelper.MakePostWebRequest<EmployeeModel, Jsend<EmployeeModel>>(
                        $"{apiDomain}/api/Employee", data);
                    return Jsend(result);
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex);
                }
            }
            List<ValidationFailure> failures = validateResult.Errors.ToList();
            return Jsend(JsendResult<List<ValidationFailure>>.Fail(failures));
        }


        [HttpPost]
        public ActionResult UpdateEmployee(EmployeeModel data)
        {
            if (data == null) return Jsend(JsendResult<List<ValidationFailure>>.Error("Employee data can't be null"));
            var validator = new EmployeeValidator();
            ValidationResult validateResult = validator.Validate(data);

            if (validateResult.IsValid)
            {
                try
                {
                    var result = RequestHelper.MakePostWebRequest<EmployeeModel, Jsend<EmployeeModel>>(
                    $"{apiDomain}/api/Employee", data, "PUT");
                    return Jsend(result);
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex);
                }
            }
            List<ValidationFailure> failures = validateResult.Errors.ToList();
            return Jsend(JsendResult<List<ValidationFailure>>.Fail(failures));
        }

        [HttpPost]
        public ActionResult DeleteEmployeeByID(int id)
        {
            var data = RequestHelper.MakePostWebRequest<EmployeeModel, Jsend<EmployeeModel>>(
                            $"{apiDomain}/api/Employee", new EmployeeModel { EmployeeID = id }, "DELETE");
            return Jsend(data);
        }
    }
}