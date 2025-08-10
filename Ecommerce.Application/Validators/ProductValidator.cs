using Ecommerce.Application.Dto;
using FluentValidation;


namespace Ecommerce.Application.Validators
{
    public class ProductValidator:AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("name must not be Not Empty")
                .MinimumLength(2).WithMessage("name must not be less than 2")
                .MaximumLength(50).WithMessage("name must not be more than 50");
            RuleFor(x => x.Description)
                .MaximumLength(400).WithMessage("name must not be more than 50");

            RuleFor(x => x.Stock)
               .NotEmpty().WithMessage("stock must not empty").
               GreaterThanOrEqualTo(1).WithMessage("stock must be at least 1");
            RuleFor(x => x.Price)
               .NotEmpty().WithMessage("price must not empty").
               GreaterThan(0).WithMessage("price must be at least more than one");

            RuleFor(x => x.CategoryId)
               .NotEmpty().WithMessage("CategoryId must not empty").
               GreaterThanOrEqualTo(1).WithMessage("CategoryId must be a positive integer");
            RuleFor(x => x.ProductImages)
              .NotEmpty().WithMessage("ProductImages must not empty");
        }
    }
}