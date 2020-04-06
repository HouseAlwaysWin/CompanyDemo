using CompanyApiService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApiService.Services
{
    public interface IProductService
    {
        Jsend AddProduct(ProductModel data);

        Jsend FindProductByName(string name);

        Jsend FindProductByID(int Id);

        Jsend UpdateProduct(ProductModel data);
        Jsend InsertUpdateProduct(ProductModel data);


        Jsend DeleteProductByID(int id);

        Jsend DeleteProduct(ProductModel data);
    }
}
