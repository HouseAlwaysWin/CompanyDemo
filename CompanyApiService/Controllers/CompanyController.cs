using CompanyApiService.Models;
using CompanyApiService.Services;
using DBAccess.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CompanyApiService.Controllers
{
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

            string resultString = JsonConvert.SerializeObject(result);
            if (result.status == JsendResultStatus.success.ToString())
            {
                return Ok(resultString);
            }
            return BadRequest(resultString);
        }


        [HttpGet]
        public IHttpActionResult GetCompanyByID(string name)
        {
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetCompanyByName(string name)
        {
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update()
        {
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete()
        {
            return Ok();
        }


    }
}
