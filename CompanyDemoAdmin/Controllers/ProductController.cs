﻿using CompanyDemo.Domain.DTOs;
using CompanyDemoAdmin.Controllers.Base;
using CompanyDemoAdmin.Helpers;
using CompanyDemoAdmin.Models;
using CompanyDemoAdmin.Models.Validators;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CompanyDemoAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetProductListByID(int page, int? searchText, bool isDesc = false, string sortBy = "ProductID")
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<ProductModel>>>(
                    $"https://localhost:44319/api/Product/GetProductListByID?current={page}&itemsPerPages=10&&searchText={searchText}&isDesc={isDesc}&&sortBy={sortBy}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("GetProductList occured error"));
            }
        }

        [HttpGet]
        public ActionResult GetProductListByName(int page, string searchText, bool isDesc = false, string sortBy = "ProductID")
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<ProductModel>>>(
                    $"https://localhost:44319/api/Product/GetProductListByName?current={page}&itemsPerPages=10&&searchText={searchText}&isDesc={isDesc}&&sortBy={sortBy}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("GetProductList occured error"));
            }
        }


        [HttpGet]
        public ActionResult GetProductByName(string name)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<ProductModel>>(
                    $"https://localhost:44319/api/Product/GetProductByName?name={name}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("GetProductList occured error"));
            }
        }


        [HttpGet]
        public ActionResult GetProductByID(string id)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<ProductModel>>>(
                    $"https://localhost:44319/api/Product/?id={id}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("GetProductList occured error"));
            }
        }


        [HttpGet]
        public ActionResult GetListByCompanyID(int id, int currentPage, bool isDesc)
        {
            try
            {
                var result = RequestHelper.MakeGetWebRequest<Jsend<OneToManyMap<ProductModel, CompanyModel>>>(
                    $"https://localhost:44319/api/Product/GetListByCompanyID?id={id}&currentPage={currentPage}&itemsPerPage=10&isDesc={isDesc}");
                return Jsend(result);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return Jsend(JsendResult.Error("GetListByCompanyID occured error"));
            }
        }


        [HttpPost]
        public ActionResult AddProduct(ProductModel data)
        {
            if (data == null) return Jsend(JsendResult<List<ValidationFailure>>.Error("Product data can't be null"));
            var validator = new ProductValidator();
            ValidationResult validateResult = validator.Validate(data);

            if (validateResult.IsValid)
            {
                try
                {
                    var result = RequestHelper.MakePostWebRequest<ProductModel, Jsend<ProductModel>>(
                        $"https://localhost:44319/api/Product", data);
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
        public ActionResult UpdateProduct(ProductModel data)
        {
            if (data == null) return Jsend(JsendResult<List<ValidationFailure>>.Error("Product data can't be null"));
            var validator = new ProductValidator();
            ValidationResult validateResult = validator.Validate(data);

            if (validateResult.IsValid)
            {
                try
                {
                    var result = RequestHelper.MakePostWebRequest<ProductModel, Jsend<ProductModel>>(
                    $"https://localhost:44319/api/Product", data, "PUT");
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
        public ActionResult DeleteProductByID(int id)
        {
            var data = RequestHelper.MakePostWebRequest<ProductModel, Jsend<ProductModel>>(
                            $"https://localhost:44319/api/Product", new ProductModel { ProductID = id }, "DELETE");
            return Jsend(data);
        }
    }
}