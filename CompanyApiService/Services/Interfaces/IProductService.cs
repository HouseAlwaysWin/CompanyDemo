using CompanyApiService.Models;
using CompanyDemo.Domain.DTOs;
using CompanyDemo.Domain.Entities;
using FluentValidation.Results;
using System.Collections.Generic;

namespace CompanyApiService.Services.Interfaces
{
    public interface IProductService
    {
        Jsend<List<ValidationFailure>> AddProduct(ProductModel data);

        Jsend<ProductT> FindProductByName(string name);

        Jsend<ProductT> FindProductByID(int Id);

        Jsend<List<ValidationFailure>> UpdateProduct(ProductModel data);
        Jsend<List<ValidationFailure>> InsertUpdateProduct(ProductModel data);


        Jsend DeleteProduct(int id);

        Jsend DeleteProduct(ProductModel data);
    }
}
