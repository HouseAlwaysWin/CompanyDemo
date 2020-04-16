using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyApiService.Models.Validators
{
    public class CompanyValidator : AbstractValidator<CompanyModel>
    {
        public CompanyValidator()
        {
            //RuleFor(c => c.CompanyName).MaximumLength(50).NotEmpty();
            //RuleFor(c => c.CompanyCode).MaximumLength(50).NotEmpty();
            //RuleFor(c => c.TaxID).Length(8).NotEmpty();
            //RuleFor(c => c.Phone).MaximumLength(20);
            //RuleFor(c => c.Address).MaximumLength(100);
            //RuleFor(c => c.WebsiteURL).MaximumLength(320);
            //RuleFor(c => c.Owner).MaximumLength(50);
            //RuleFor(c => c.Owner).MaximumLength(50);
        }
    }
}