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
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly Context _context;
        public LoginController(IConfiguration config,Context context)
        {
            _config = config;
            _context = context;
        }
        [HttpPost("logintoken")]
        public async Task<IActionResult> Login(LoginModel loginViewModel)
        {
            try
            {
                string jwtToken = await Logintoken(loginViewModel);
                return Ok(new { jwtToken });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        private async Task<string> Logintoken(LoginModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Username == model.UserName && model.Password == p.Password);
            if (user != null)
            {
                //tạo ra jwt string để gửi cho client
                // Nếu xác thực thành công, tạo JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["AppSettings:SecretKey"] ?? "");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, "Admin")
                    }),
                    Expires = DateTime.UtcNow.AddDays(30),
                    SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                return jwtToken;
            }
            else
            {
                throw new ArgumentException("Wrong email or password");
            }
        }
    }
}
