using CompanyApiService.Models;
using CompanyApiService.Services.Interfaces;
using CompanyDemo.Domain.DTOs;
using Newtonsoft.Json;
using System.Web.Http;

namespace CompanyApiService.Controllers
{
    public class ProductController : ApiController
    {
        private IProductService _productService;
        public ProductController(IProductService ProductService)
        {
            _productService = ProductService;
        }

        [HttpPost]
        public IHttpActionResult AddProduct(ProductModel data)
        {
            var result = _productService.AddProduct(data);

            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpGet]
        public IHttpActionResult GetProductByID(int id)
        {
            var result = _productService.FindProductByID(id);
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }


        //[HttpGet]
        //public IHttpActionResult FindAll(int currentPage, int itemsPerPage, bool isDesc = false)
        //{
        //    var result = _productService.FindAll(currentPage, itemsPerPage, isDesc);
        //    if (result.status == EnumJsendStatus.success.ToString())
        //    {
        //        return Ok(result);
        //    }

        //    string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        //    return BadRequest(resultString);
        //}


        [HttpGet]
        [Route("api/Product/GetListByCompanyID")]
        public IHttpActionResult GetListByCompanyID(int id, int currentPage, int itemsPerPage, bool isDesc)
        {
            var result = _productService.FindCompanyListByID(id, currentPage, itemsPerPage, isDesc);
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }


        [HttpGet]
        [Route("api/Product/GetListByCompanyName")]
        public IHttpActionResult GetListByCompanyName(string searchText, int currentPage, int itemsPerPage, bool isDesc)
        {
            var result = _productService.FindAllByCompanyName(searchText, currentPage, itemsPerPage, isDesc);
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }


        [HttpGet]
        [Route("api/Product/GetListByProductType")]
        public IHttpActionResult GetListByProductType(string searchText, int currentPage, int itemsPerPage, bool isDesc)
        {
            var result = _productService.FindAllByProductType(searchText, currentPage, itemsPerPage, isDesc);
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }


        [HttpGet]
        [Route("api/Product/GetListByProductPrice")]
        public IHttpActionResult GetListByProductType(int? searchText, int currentPage, int itemsPerPage, bool isDesc)
        {
            var result = _productService.FindAllByProductPrice(searchText, currentPage, itemsPerPage, isDesc);
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpPatch]
        public IHttpActionResult Update(ProductModel data)
        {
            var result = _productService.UpdateProduct(data);
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpPut]
        public IHttpActionResult InsertUpdate(ProductModel data)
        {
            var result = _productService.InsertUpdateProduct(data);
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var result = _productService.DeleteProduct(id);
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpDelete]
        public IHttpActionResult Delete(ProductModel data)
        {
            var result = _productService.DeleteProduct(data);
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }
    }
}
