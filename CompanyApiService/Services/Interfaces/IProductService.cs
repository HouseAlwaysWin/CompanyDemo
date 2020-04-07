using CompanyApiService.Models;
using DBAccess.Entities;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
