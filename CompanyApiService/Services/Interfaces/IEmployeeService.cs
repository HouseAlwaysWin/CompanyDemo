﻿using CompanyApiService.Models;
using DBAccess.Entities;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApiService.Services.Interfaces
{
    public interface IEmployeeService
    {
        Jsend<List<ValidationFailure>> AddEmployee(EmployeeModel data);

        Jsend<EmployeeT> FindEmployeeByName(string name);

        Jsend<EmployeeT> FindEmployeeByID(int Id);

        Jsend<List<ValidationFailure>> UpdateEmployee(EmployeeModel data);
        Jsend<List<ValidationFailure>> InsertUpdateEmployee(EmployeeModel data);


        Jsend DeleteEmployee(int id);

        Jsend DeleteEmployee(EmployeeModel data);
    }
}