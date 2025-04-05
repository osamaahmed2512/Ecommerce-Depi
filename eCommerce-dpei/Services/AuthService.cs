using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;
using eCommerce_dpei.Data;
using eCommerce_dpei.Models;

namespace eCommerce_dpei.Services
{
    public class AuthService
    {
        private readonly EcommerceContext _context;
        private readonly IConfiguration _config;

        public AuthService(EcommerceContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<Customer> RegisterCustomer(RegisterDto dto)
        {
            var customer = new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Phone = dto.Phone
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<string> Login(LoginDto dto)
        {
            // Check customer
            var customer = _context.Customers.FirstOrDefault(c => c.Email == dto.Email);
            if (customer != null && BCrypt.Net.BCrypt.Verify(dto.Password, customer.Password))
            {
                return GenerateJwtToken(customer.Id.ToString(), customer.Email, "Customer");
            }

            // Check admin
            var admin = _context.Admins.FirstOrDefault(a => a.Email == dto.Email);
            if (admin != null && BCrypt.Net.BCrypt.Verify(dto.Password, admin.Password))
            {
                return GenerateJwtToken(admin.Id.ToString(), admin.Email, "Admin");
            }

            throw new Exception("Invalid credentials");
        }

        private string GenerateJwtToken(string id, string email, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}