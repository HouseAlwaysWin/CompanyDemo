using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyApiService.Models.Validators
{
    public class ProductValidator : AbstractValidator<ProductModel>
    {
        public ProductValidator()
        {
            //RuleFor(p => p.ProductName).MaximumLength(100).NotEmpty();
            //RuleFor(p => p.ProductType).MaximumLength(50).NotEmpty();
            //RuleFor(p => p.Unit).MaximumLength(10);
        }
    }
}