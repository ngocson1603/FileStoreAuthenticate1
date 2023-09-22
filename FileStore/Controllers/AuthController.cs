using FileStore.Models;
using FileStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FileStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Context _context;
        public AuthController(IConfiguration configuration, Context context)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Validate(LoginModel model)
        {
            var user = _context.Users.SingleOrDefault(p => p.Username == model.UserName && model.Password == p.Password);
            if (user == null) //không đúng
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid username/password"
                });
            }

            //cấp token
            string token = GenerateToken(user);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Authenticate success",
                Data = token
            });
        }
        private string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,"Admin")
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.
                UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
