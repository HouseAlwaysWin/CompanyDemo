using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyApiService.Models.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeModel>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.EmployeeName).MaximumLength(50).NotEmpty();
            RuleFor(e => e.Email).MaximumLength(320).EmailAddress().NotEmpty();
            RuleFor(e => e.BirthdayDate).NotEmpty();
            RuleFor(e => e.SignInDate).NotEmpty();
            RuleFor(e => e.Salary).NotNull();
        }
    }
}