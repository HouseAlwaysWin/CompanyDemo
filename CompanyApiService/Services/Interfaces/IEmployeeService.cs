using CompanyApiService.Models;
using CompanyDemo.Domain.DTOs;
using CompanyDemo.Domain.Entities;
using FluentValidation.Results;
using System.Collections.Generic;

namespace CompanyApiService.Services.Interfaces
{
    public interface IEmployeeService
    {
        Jsend<List<ValidationFailure>> AddEmployee(EmployeeModel data);

        Jsend<EmployeeT> FindEmployeeByName(string name);

        Jsend<EmployeeT> FindEmployeeByID(int Id);

        Jsend<OneToManyMap<EmployeeT, CompanyT>> FindCompanyListByID(int id, int current, int itemsPerPages, bool isDesc);

        Jsend<List<ValidationFailure>> UpdateEmployee(EmployeeModel data);
        Jsend<List<ValidationFailure>> InsertUpdateEmployee(EmployeeModel data);


        Jsend DeleteEmployee(int id);

        Jsend DeleteEmployee(EmployeeModel data);
    }
}
