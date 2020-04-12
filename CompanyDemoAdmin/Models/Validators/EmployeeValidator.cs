using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDemoAdmin.Models.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeModel>
    {
        public EmployeeValidator()
        {
        }
    }
}