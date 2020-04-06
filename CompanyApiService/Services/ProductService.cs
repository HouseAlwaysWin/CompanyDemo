using CompanyApiService.Models;
using CompanyApiService.Models.Validators;
using CompanyApiService.Services.Interfaces;
using DBAccess;
using DBAccess.Entities;
using FluentValidation.Results;
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
        public Jsend AddProduct(ProductModel data)
        {
            if (data == null) return JsendResult.Error("AddProduct() data can't be null");

            var validator = new ProductValidator();
            ValidationResult validateResult = validator.Validate(data);
            if (validateResult.IsValid)
            {
                try
                {
                    _uow.ProductTRepository.Add(new ProductT
                    {
                        ProductName = data.ProductName,
                        ProductType = data.ProductType,
                        Price = data.Price,
                        Unit = data.Unit
                    });
                    _uow.Commit();
                    return JsendResult.Success();
                }
                catch (SqlException ex)
                {
                    _logger.Error(ex);
                    return JsendResult.Error("AddProduct Insert Product occured error");
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

        public Jsend FindProductByID(int id)
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
                return JsendResult.Error("FindProductByID() Query data occured error.");
            }
            return JsendResult<ProductT>.Success(result);
        }

        public Jsend FindProductByName(string name)
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
                return JsendResult.Error("FindProductByName() occured error");
            }
            return JsendResult<ProductT>.Success(result);
        }

        public Jsend InsertUpdateProduct(ProductModel data)
        {
            if (data == null) return JsendResult.Error("Product data can't be null");
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
                    return JsendResult.Error("InsertUpdateProduct() InsertUpdate data occured error");
                }
                return JsendResult.Success();
            }

            List<ValidationFailure> failures = validateResult.Errors.ToList();

            return JsendResult<List<ValidationFailure>>.Fail(failures);
        }

        public Jsend UpdateProduct(ProductModel data)
        {
            if (data == null) return JsendResult.Error("Product data can't be null");
            var validator = new ProductValidator();
            ValidationResult validateResult = validator.Validate(data);
            if (validateResult.IsValid)
            {
                try
                {
                    _uow.ProductTRepository.Update(new ProductT
                    {
                        ProductName = data.ProductName,
                        ProductType = data.ProductType,
                        Price = data.Price,
                        Unit = data.Unit
                    });
                    _uow.Commit();
                }
                catch (SqlException ex)
                {
                    _logger.Error(ex);
                    return JsendResult.Error("InsertUpdateProduct() InsertUpdate data occured error");
                }
                return JsendResult.Success();
            }

            List<ValidationFailure> failures = validateResult.Errors.ToList();

            return JsendResult<List<ValidationFailure>>.Fail(failures);
        }
    }
}