using Ecommerce.Application.Dto;
using FluentValidation;

namespace Ecommerce.Application.Validators
{
    public class LoginDtoValidator:AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Inavlid Email Format")
                .MaximumLength(180);
            RuleFor(x => x.Password)
                .MinimumLength(8).WithMessage("length must not less than 8")
                .NotEmpty().WithMessage("password is required");
                    
        }
    }
}
