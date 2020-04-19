using CompanyApiService.Models;
using CompanyApiService.Models.Filters;
using CompanyApiService.Services.Interfaces;
using CompanyDemo.Domain.DTOs;
using Newtonsoft.Json;
using System.Web.Http;

namespace CompanyApiService.Controllers
{
    [BasicAuthentication]
    public class CompanyController : ApiController
    {
        private ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }


        [HttpPost]
        public IHttpActionResult AddNewCompany(CompanyModel data)
        {
            var result = _companyService.AddCompany(data);

            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpGet]
        [Route("api/Company/GetCompanyListByID")]
        public IHttpActionResult GetCompanyListByID(int current, int itemsPerPage, bool isDesc, string searchText)
        {
            var result = _companyService.FindCompanyListByID(current, itemsPerPage, isDesc, searchText);
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpGet]
        [Route("api/Company/GetCompanyListByName")]
        public IHttpActionResult GetCompanyListByName(int current, int itemsPerPage, bool isDesc, string searchText)
        {
            var result = _companyService.FindCompanyListByName(current, itemsPerPage, isDesc, searchText);
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpPatch]
        public IHttpActionResult Update(CompanyModel data)
        {
            var result = _companyService.UpdateCompany(data);
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpPut]
        public IHttpActionResult InsertUpdate(CompanyModel data)
        {
            var result = _companyService.InsertUpdateCompany(data);
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
            var result = _companyService.DeleteCompanyByID(id);
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpDelete]
        public IHttpActionResult Delete(CompanyModel data)
        {
            var result = _companyService.DeleteCompany(data);
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }


    }
}
