using FileStore.Models;
using FileStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FileStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly Context context;
        private readonly AppSetting jwtSettings;
        public AuthorizeController(Context context,IOptions<AppSetting> options)
        {
            this.context = context;
            this.jwtSettings = options.Value;
        }
        [HttpPost("GenerateToken")]
        public async Task<IActionResult> GenerateToken([FromBody] LoginModel userCred)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(item => item.Username == userCred.UserName && item.Password == userCred.Password);
            if (user != null)
            {
                //generate token
                var tokenhandler = new JwtSecurityTokenHandler();
                var tokenkey = Encoding.UTF8.GetBytes(this.jwtSettings.SecretKey);
                var tokendesc = new SecurityTokenDescriptor
                {
                    Subject=new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name,user.Username),
                        new Claim(ClaimTypes.Role,user.Email)
                    }),
                    Expires=DateTime.UtcNow.AddDays(30),
                    SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(tokenkey),SecurityAlgorithms.HmacSha256)
                };
                var token = tokenhandler.CreateToken(tokendesc);
                var finaltoken = tokenhandler.WriteToken(token);
                return Ok(new TokenResponse() { Token = finaltoken});

            }
            else
            {
                return Unauthorized();
            }
            
        }

        //[HttpPost("GenerateRefreshToken")]
        //public async Task<IActionResult> GenerateToken([FromBody] TokenResponse token)
        //{
        //    var _refreshtoken = await this.context.TblRefreshtokens.FirstOrDefaultAsync(item => item.Refreshtoken == token.RefreshToken);
        //    if (_refreshtoken != null)
        //    {
        //        //generate token
        //        var tokenhandler = new JwtSecurityTokenHandler();
        //        var tokenkey = Encoding.UTF8.GetBytes(this.jwtSettings.securitykey);
        //        SecurityToken securityToken;
        //        var principal = tokenhandler.ValidateToken(token.Token, new TokenValidationParameters()
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(tokenkey),
        //            ValidateIssuer = false,
        //            ValidateAudience = false,

        //        }, out securityToken);

        //        var _token = securityToken as JwtSecurityToken;
        //        if(_token != null  && _token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
        //        {
        //            string username = principal.Identity?.Name;
        //            var _existdata=await this.context.TblRefreshtokens.FirstOrDefaultAsync(item=>item.Userid==username
        //            && item.Refreshtoken==token.RefreshToken);
        //            if (_existdata != null)
        //            {
        //                var _newtoken = new JwtSecurityToken(
        //                    claims:principal.Claims.ToArray(),
        //                    expires:DateTime.Now.AddSeconds(30),
        //                    signingCredentials:new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtSettings.securitykey)),
        //                    SecurityAlgorithms.HmacSha256)
        //                    );

        //                var _finaltoken = tokenhandler.WriteToken(_newtoken);
        //                return Ok(new TokenResponse() { Token = _finaltoken, RefreshToken = await this.refresh.GenerateToken(username) });
        //            }
        //            else
        //            {
        //                return Unauthorized();
        //            }
        //        }
        //        else
        //        {
        //            return Unauthorized();
        //        }

        //        //var tokendesc = new SecurityTokenDescriptor
        //        //{
        //        //    Subject = new ClaimsIdentity(new Claim[]
        //        //    {
        //        //        new Claim(ClaimTypes.Name,user.Code),
        //        //        new Claim(ClaimTypes.Role,user.Role)
        //        //    }),
        //        //    Expires = DateTime.UtcNow.AddSeconds(30),
        //        //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
        //        //};
        //        //var token = tokenhandler.CreateToken(tokendesc);
        //        //var finaltoken = tokenhandler.WriteToken(token);
        //        //return Ok(new TokenResponse() { Token = finaltoken, RefreshToken = await this.refresh.GenerateToken(userCred.username) });

        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }

        //}
    }
}
