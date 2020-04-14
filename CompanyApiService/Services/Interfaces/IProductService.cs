using System.Collections.Generic;
using CompanyApiService.Models;
using CompanyDemo.Domain.DTOs;
using CompanyDemo.Domain.Entities;
using FluentValidation.Results;

namespace CompanyApiService.Services.Interfaces
{
    public interface IProductService
    {
        Jsend<List<ValidationFailure>> AddProduct(ProductModel data);
        Jsend DeleteProduct(int id);
        Jsend DeleteProduct(ProductModel data);
        Jsend<OneToManyMap<ProductT, CompanyT>> FindCompanyListByID(int id, int current, int itemsPerPages, bool isDesc);
        Jsend<ProductT> FindProductByID(int id);
        Jsend<ProductT> FindProductByName(string name);
        Jsend<List<ValidationFailure>> InsertUpdateProduct(ProductModel data);
        Jsend<List<ValidationFailure>> UpdateProduct(ProductModel data);
    }
}