using CompanyApiService.Models;
using CompanyApiService.Models.Validators;
using CompanyApiService.Services.Interfaces;
using CompanyDemo.Domain.DTOs;
using CompanyDemo.Domain.Entities;
using DBAccess;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CompanyApiService.Services
{
    public class ProductService : IProductService
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        IUnitOfWork _uow;
        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public Jsend<List<ValidationFailure>> AddProduct(ProductModel data)
        {
            if (data == null) return JsendResult<List<ValidationFailure>>.Error("Product data can't be null");

            var validator = new ProductValidator();
            ValidationResult validateResult = validator.Validate(data);
            if (validateResult.IsValid)
            {
                try
                {
                    _uow.ProductTRepository.Add(new ProductT
                    {
                        CompanyID = data.CompanyID,
                        ProductName = data.ProductName,
                        ProductType = data.ProductType,
                        Price = data.Price,
                        Unit = data.Unit
                    });
                    _uow.Commit();
                    return JsendResult<List<ValidationFailure>>.Success();
                }
                catch (SqlException ex)
                {
                    _logger.Error(ex);
                    return JsendResult<List<ValidationFailure>>.Error("Insert Product occured error");
                }
            }
            List<ValidationFailure> failures = validateResult.Errors.ToList();

            return JsendResult<List<ValidationFailure>>.Fail(failures);
        }

        public Jsend DeleteProduct(ProductModel data)
        {
            if (data == null) return JsendResult.Error("Product data can't be null");
            try
            {
                _uow.ProductTRepository.Delete(new ProductT
                {
                    ProductID = data.ProductID
                });
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult.Error("DeleteProduct() Delete data occured error.");
            }
            return JsendResult.Success();
        }

        public Jsend DeleteProduct(int id)
        {
            try
            {
                _uow.ProductTRepository.Delete(new ProductT
                {
                    ProductID = id
                });
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult.Error("DeleteProduct() Delete data occured error.");
            }
            return JsendResult.Success();
        }

        public Jsend<ProductT> FindProductByID(int id)
        {
            ProductT result = null;

            try
            {
                result = _uow.ProductTRepository.FindByID(id);
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult<ProductT>.Error("");
            }
            return JsendResult<ProductT>.Success(result);
        }

        public Jsend<ProductT> FindProductByName(string name)
        {
            ProductT result = null;

            try
            {
                result = _uow.ProductTRepository.FindByName(name);
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult<ProductT>.Error("FindProductByName() occured error");
            }
            return JsendResult<ProductT>.Success(result);
        }


        public Jsend<OneToManyMap<ProductT, CompanyT>> FindCompanyListByID(int id, int current, int itemsPerPages, bool isDesc)
        {
            OneToManyMap<ProductT, CompanyT> result = null;
            try
            {
                result = _uow.ProductTRepository.FindAllByCompanyID(id, current, itemsPerPages, isDesc);
                _uow.Commit();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex);
                return JsendResult<OneToManyMap<ProductT, CompanyT>>.Error("Queay data occured error");
            }
            return JsendResult<OneToManyMap<ProductT, CompanyT>>.Success(result);
        }

        public Jsend<List<ValidationFailure>> InsertUpdateProduct(ProductModel data)
        {
            if (data == null) return JsendResult<List<ValidationFailure>>.Error("Product data can't be null");
            var validator = new ProductValidator();
            ValidationResult validateResult = validator.Validate(data);

            if (validateResult.IsValid)
            {
                try
                {
                    var result = _uow.ProductTRepository.FindByID(data.ProductID);
                    if (result == null)
                    {
                        _uow.ProductTRepository.Add(new ProductT
                        {
                            ProductID = data.ProductID,
                            ProductName = data.ProductName,
                            ProductType = data.ProductType,
                            Price = data.Price,
                            Unit = data.Unit
                        });
                    }
                    else
                    {
                        _uow.ProductTRepository.Update(new ProductT
                        {
                            ProductID = data.ProductID,
                            ProductName = data.ProductName,
                            ProductType = data.ProductType,
                            Price = data.Price,
                            Unit = data.Unit

                        });
                    }
                    _uow.Commit();
                }
                catch (SqlException ex)
                {
                    _logger.Error(ex);
                    return JsendResult<List<ValidationFailure>>.Error("InsertUpdateProduct() InsertUpdate data occured error");
                }
                return JsendResult<List<ValidationFailure>>.Success();
            }

            List<ValidationFailure> failures = validateResult.Errors.ToList();

            return JsendResult<List<ValidationFailure>>.Fail(failures);
        }

        public Jsend<List<ValidationFailure>> UpdateProduct(ProductModel data)
        {
            if (data == null) return JsendResult<List<ValidationFailure>>.Error("Product data can't be null");
            var validator = new ProductValidator();
            ValidationResult validateResult = validator.Validate(data);
            if (validateResult.IsValid)
            {
                try
                {
                    _uow.ProductTRepository.Update(new ProductT
                    {
                        ProductID = data.ProductID,
                        ProductName = data.ProductName,
                        ProductType = data.ProductType,
                        Price = data.Price,
                        Unit = data.Unit,
                        EditedDate = DateTime.UtcNow
                    });
                    _uow.Commit();
                }
                catch (SqlException ex)
                {
                    _logger.Error(ex);
                    return JsendResult<List<ValidationFailure>>.Error("InsertUpdateProduct() InsertUpdate data occured error");
                }
                return JsendResult<List<ValidationFailure>>.Success();
            }

            List<ValidationFailure> failures = validateResult.Errors.ToList();

            return JsendResult<List<ValidationFailure>>.Fail(failures);
        }
    }
}