using CompanyApiService.Models;
using CompanyApiService.Services.Interfaces;
using Newtonsoft.Json;
using System.Web.Http;

namespace CompanyApiService.Controllers
{
    public class ProductController : ApiController
    {
        private IProductService _ProductService;
        public ProductController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }

        [HttpPost]
        public IHttpActionResult AddProduct(ProductModel data)
        {
            var result = _ProductService.AddProduct(data);

            if (result.status == JsendResultStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpGet]
        [Route("api/Product/GetCompanyByID")]
        public IHttpActionResult GetCompanyByID(int id)
        {
            var result = _ProductService.FindProductByID(id);
            if (result.status == JsendResultStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpGet]
        public IHttpActionResult GetCompanyByName(string name)
        {
            var result = _ProductService.FindProductByName(name);
            if (result.status == JsendResultStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpPatch]
        public IHttpActionResult Update(ProductModel data)
        {
            var result = _ProductService.UpdateProduct(data);
            if (result.status == JsendResultStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpPut]
        public IHttpActionResult InsertUpdate(ProductModel data)
        {
            var result = _ProductService.InsertUpdateProduct(data);
            if (result.status == JsendResultStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var result = _ProductService.DeleteProduct(id);
            if (result.status == JsendResultStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpDelete]
        public IHttpActionResult Delete(ProductModel data)
        {
            var result = _ProductService.DeleteProduct(data);
            if (result.status == JsendResultStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }
    }
}
