using Ecommerce.Application.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using FluentValidation;
using Ecommerce.Application.Dto;
using Ecommerce.Application.mapping;
namespace Ecommerce.Application
{
    
    public static class ApplicationRegisteration
    {
        public static IServiceCollection ApplicationRegisterationService(this IServiceCollection services) 
        {
            services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            //services.AddFluentValidationAutoValidation();

            //services.AddScoped<IValidator<RegisterDto>, RegisterDtoValidator>();
            return services;
        }
    }
}
