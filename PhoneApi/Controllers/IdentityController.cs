using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PhoneApi.Models;
using PhoneApi.Services.Interfaces;
using PhoneApi.Utils.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApi.Controllers
{
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        IIdentityService identityService;

        public IdentityController(IIdentityService service)
        {
            identityService = service;
        }

        [Route("token")]
        [HttpPost]
        public async Task<IActionResult> Token([FromBody]User user)
        {
            var identity = GetIdentity(user.Login, user.Password);
            if (identity == null)
            {
                return Unauthorized();
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedJwt,
                login = identity.Name
            };
            return Ok(response);
        }

        private ClaimsIdentity GetIdentity(string login, string password)
        {
            ClaimsIdentity identity = null;
            var user = identityService.GetUser(login);
            if (user != null)
            {
                var sha256 = new SHA256Managed();
                var passwordHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
                if (passwordHash == user.Password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
                    };
                    identity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                }
            }
            return identity;
        }
    }
}
