using Ecommerce.Application.Dto;
using FluentValidation;

namespace Ecommerce.Application.Validators
{
    public class RegisterDtoValidator: AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50)
                 .Matches("^[a-zA-Zأ-يءئآإة]+$").WithMessage("First name must contain only letters and no numbers.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50)
                 .Matches("^[a-zA-Zأ-يءئآإة]+$").WithMessage("Last name must contain only letters and no numbers.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(80);
            
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .Matches("[A-Z]").WithMessage("Must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Must contain at least one number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Must contain at least one special character");

            RuleFor(x => x.Phone)
                .MaximumLength(20)
                .Matches(@"^\+?\d{10,}$").When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Invalid phone number format.");
        }
    }
}
