using CompanyApiService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApiService.Services
{
    public interface IEmployeeService
    {
        Jsend AddEmployee(EmployeeModel data);

        Jsend FindEmployeeByName(string name);

        Jsend FindEmployeeByID(int Id);

        Jsend UpdateEmployee(EmployeeModel data);
        Jsend InsertUpdateEmployee(EmployeeModel data);


        Jsend DeleteEmployeeByID(int id);

        Jsend DeleteEmployee(EmployeeModel data);
    }
}
