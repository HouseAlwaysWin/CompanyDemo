using CompanyApiService.Models;
using CompanyApiService.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CompanyApiService.Controllers
{
    public class EmployeeController : ApiController
    {
        private IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public IHttpActionResult AddEmployee(EmployeeModel data)
        {
            var result = _employeeService.AddEmployee(data);

            if (result.status == JsendResultStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }
    }
}
