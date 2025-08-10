using Ecommerce.Application.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Validators
{
    public class categoryDtoValidator:AbstractValidator<CategoryDto>
    {
        public categoryDtoValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name Is Required")
                .MaximumLength(50);
        }
    }
}
