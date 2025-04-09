using BCrypt.Net;
using eCommerce_dpei.Data;
using eCommerce_dpei.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly EcommerceContext _context;
        private readonly IConfiguration _config;

        public AuthController(EcommerceContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("register/customer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); 
                }

                if (_context.Customers.Any(c => c.Email == dto.Email))
                {
                    return BadRequest(new { Message = "Email already in use" });
                }

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

                return Ok(new { Message = "Customer registered successfully", CustomerId = customer.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); // Returns validation errors
                }

                var customer = _context.Customers.FirstOrDefault(c => c.Email == dto.Email);
                if (customer != null && BCrypt.Net.BCrypt.Verify(dto.Password, customer.Password))
                {
                    var token = GenerateJwtToken(customer.Id.ToString(), customer.Email, "Customer");
                    return Ok(new { Token = token });
                }

                var admin = _context.Admins.FirstOrDefault(a => a.Email == dto.Email);
                if (admin != null && BCrypt.Net.BCrypt.Verify(dto.Password, admin.Password))
                {
                    var token = GenerateJwtToken(admin.Id.ToString(), admin.Email, "Admin");
                    return Ok(new { Token = token });
                }

                return Unauthorized(new { Message = "Invalid credentials" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
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

        [HttpGet("me")]
        [Authorize] 
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest(new { Message = "Invalid user ID claim." });
            }

            var userRoleClaim = User.FindFirst(ClaimTypes.Role);
            if (userRoleClaim == null)
            {
                return BadRequest(new { Message = "User role not found in the token." });
            }
            var userRole = userRoleClaim.Value;


            object user = null;
            try
            {
                if (userRole == "Customer")
                {
                    user = await _context.Customers.FindAsync(userId);
                }
                else if (userRole == "Admin")
                {
                    user = await _context.Admins.FindAsync(userId);
                }
                else
                {
                    return BadRequest(new { Message = "Invalid user role." });
                }

                if (user == null)
                {
                    return NotFound(new { Message = "User not found." });
                }

                return Ok(user); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error fetching user: {ex.Message}" });
            }
        }
    }
}