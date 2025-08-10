using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Interfaces.Repository;
using Ecommerce.Application.services;
using Ecommerce.infrastructure.Identity;
using Ecommerce.infrastructure.percistence;
using Ecommerce.infrastructure.percistence.Repository;
using Ecommerce.infrastructure.services;
using Ecommerce.infrastructure.services.PayPalSetting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ecommerce.infrastructure
{
    public static class InfrasructureRegisteration
    {
        public static void InfrastructureRegisteration( this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplcationDbContext>()
            .AddDefaultTokenProviders();
            builder.Services.AddAuthentication(

    options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

            builder.Services.AddScoped<ItokenService, TokenService>();
            builder.Services.AddScoped<IAuthservice, AuthService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IproductService, ProductService>();
            builder.Services.AddScoped<IcartService, CartService>();
            builder.Services.AddScoped<IUnitofwork,Unitofwork>();
            builder.Services.AddScoped<IAdressService,AddressService>();
            builder.Services.Configure<PayPalSettings>(builder.Configuration.GetSection("PayPalSettings"));
            builder.Services.AddHttpClient<IPaymentService, PayPalService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
        }
    }
}
