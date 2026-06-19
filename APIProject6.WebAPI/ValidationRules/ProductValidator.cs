using APIProject6.WebAPI.Entities;
using FluentValidation;

namespace APIProject6.WebAPI.ValidationRules
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product name is required.");
            RuleFor(x => x.ProductName).MinimumLength(2).WithMessage("Product name must be at least 2 characters long.");
            RuleFor(x => x.ProductName).MaximumLength(50).WithMessage("Product name must be less than 50 characters long.");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Product price is required.").GreaterThan(0).WithMessage("Product price must be a positive value.").
            LessThan(1000).WithMessage("Product price must not be that high");
            RuleFor(x => x.ProductDescription).NotEmpty().WithMessage("Product description is required.");

        }
    }
}
