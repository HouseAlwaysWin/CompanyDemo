using CompanyApiService.Models;
using CompanyApiService.Models.Filters;
using CompanyApiService.Services.Interfaces;
using CompanyDemo.Domain.DTOs;
using Newtonsoft.Json;
using System.Web.Http;

namespace CompanyApiService.Controllers
{
    [BasicAuthentication]
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

            if (result.status == EnumJsendStatus.success.ToString())
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
            if (result.status == EnumJsendStatus.success.ToString())
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
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpGet]
        [Route("api/Employee/FindAllByEmployeeName")]
        public IHttpActionResult FindAllByEmployeeName(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            var result = _employeeService.FindAllByEmployeeName(companyID, searchText, currentPage, itemsPerPage, isDesc);
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpGet]
        [Route("api/Employee/FindAllByEmployeeID")]
        public IHttpActionResult FindAllByEmployeeID(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            var result = _employeeService.FindAllByEmployeeID(companyID, searchText, currentPage, itemsPerPage, isDesc);
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpGet]
        [Route("api/Employee/FindAllByEmployeePhone")]
        public IHttpActionResult FindAllByEmployeePhone(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            var result = _employeeService.FindAllByEmployeePhone(companyID, searchText, currentPage, itemsPerPage, isDesc);
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpGet]
        [Route("api/Employee/FindAllByEmployeePosition")]
        public IHttpActionResult FindAllByEmployeePosition(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            var result = _employeeService.FindAllByEmployeePosition(companyID, searchText, currentPage, itemsPerPage, isDesc);
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

        [HttpGet]
        [Route("api/Employee/FindAllByEmployeeBirthday")]
        public IHttpActionResult FindAllByEmployeeBirthday(string companyID, string searchText, int currentPage, int itemsPerPage, bool isDesc = false)
        {
            var result = _employeeService.FindAllByEmployeeBirthday(companyID, searchText, currentPage, itemsPerPage, isDesc);
            if (result.status == EnumJsendStatus.success.ToString())
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
            if (result.status == EnumJsendStatus.success.ToString())
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
            var result = _employeeService.DeleteEmployee(id);
            if (result.status == EnumJsendStatus.success.ToString())
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
            if (result.status == EnumJsendStatus.success.ToString())
            {
                return Ok(result);
            }

            string resultString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return BadRequest(resultString);
        }

    }
}
