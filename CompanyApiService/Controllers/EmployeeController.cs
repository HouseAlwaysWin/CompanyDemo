using CompanyApiService.Models;
using CompanyApiService.Services.Interfaces;
using Newtonsoft.Json;
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

        [HttpGet]
        public IHttpActionResult GetEmployeeByID(int id)
        {
            var result = _employeeService.FindEmployeeByID(id);
            if (result.status == JsendResultStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpGet]
        [Route("api/Employee/GetListByCompanyID")]
        public IHttpActionResult GetListByCompanyID(int id, int currentPage, int itemsPerPage, bool isDesc)
        {
            var result = _employeeService.FindCompanyListByID(id, currentPage, itemsPerPage, isDesc);
            if (result.status == JsendResultStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpPatch]
        public IHttpActionResult Update(EmployeeModel data)
        {
            var result = _employeeService.UpdateEmployee(data);
            if (result.status == JsendResultStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpPut]
        public IHttpActionResult InsertUpdate(EmployeeModel data)
        {
            var result = _employeeService.InsertUpdateEmployee(data);
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
            var result = _employeeService.DeleteEmployee(id);
            if (result.status == JsendResultStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpDelete]
        public IHttpActionResult Delete(EmployeeModel data)
        {
            var result = _employeeService.DeleteEmployee(data);
            if (result.status == JsendResultStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

    }
}
