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

namespace EmployeeDemo.Controllers
{
    public class EmployeeController : BaseController
    {
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
                    $"https://localhost:44319/api/Employee/GetEmployeeListByID?current={page}&itemsPerPages=10&&searchText={searchText}&isDesc={isDesc}&&sortBy={sortBy}");
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
                    $"https://localhost:44319/api/Employee/GetEmployeeListByName?current={page}&itemsPerPages=10&&searchText={searchText}&isDesc={isDesc}&&sortBy={sortBy}");
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
                    $"https://localhost:44319/api/Employee/GetEmployeeByName?name={name}");
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
                    $"https://localhost:44319/api/Employee/?id={id}");
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
                    $"https://localhost:44319/api/Employee/GetListByCompanyID?id={id}&currentPage={currentPage}&itemsPerPage=10&isDesc={isDesc}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("GetListByCompanyID occured error"));
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
                        $"https://localhost:44319/api/Employee", data);
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
                    $"https://localhost:44319/api/Employee", data, "PUT");
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
                            $"https://localhost:44319/api/Employee", new EmployeeModel { EmployeeID = id }, "DELETE");
            return Jsend(data);
        }
    }
}