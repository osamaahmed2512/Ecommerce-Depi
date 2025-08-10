using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using eCommerce_dpei.Filters;
using Ecommerce.infrastructure.percistence;
using Ecommerce.infrastructure;
using eCommerce_dpei.infrastructure.percistence;
using Ecommerce.Application;
using eCommerce_dpei.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidatorFilter>();
}).
              ConfigureApiBehaviorOptions(options =>
              {
                  options.SuppressModelStateInvalidFilter = true;
              }
                )
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddDbContext<ApplcationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSwagger", builder =>
    {
        builder.AllowAnyOrigin() // In production, specify allowed origins (e.g., "https://localhost:7148")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});



builder.Services.AddScoped<ValidatorFilter>();

builder.InfrastructureRegisteration();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ApplicationRegisterationService();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "eCommerce API", Version = "v1" });

    // Add the security definition for JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // Add security requirement to all endpoints that require authorization
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});



var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GraduationProject API v1");
    c.RoutePrefix = string.Empty;
});

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

// Apply CORS middleware before Authentication/Authorization
app.UseCors("AllowSwagger");

// Middleware pipeline (order matters: Authentication -> Authorization -> Routing)
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
await DBSeeder.SeedDataAsync(app);
app.Run();