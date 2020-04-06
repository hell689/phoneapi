using Microsoft.AspNetCore.Mvc;
using PhoneApi.Models;
using PhoneApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> Token(User user)
        {
            var identity = await GetIdentity(user.Login, user.Password);
        }

        private Task GetIdentity(string login, string password)
        {
            ClaimsIdentity identity = null;
            var user = identityService.GetUser(login);
            if (user != null)
            {
                var sha256 = new SHA256Managed();
                var passwordHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
                if (passwordHash == user.Password)
                {

                }
            }
        }
    }
}
