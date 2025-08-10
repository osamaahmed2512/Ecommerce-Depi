using Ecommerce.Application.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Validators
{
    public class AddImageDtoValidator:AbstractValidator<AddImageDto>
    {
        public AddImageDtoValidator()
        {
            RuleFor(x =>x.Image).NotEmpty().WithMessage("please enter image");
        }
    }
}
