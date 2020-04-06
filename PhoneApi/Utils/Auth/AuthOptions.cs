using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApi.Utils.Auth
{
    public class AuthOptions
    {
        public const string ISSUER = "PhoneAuthServer";
        public const string AUDIENCE = "PhoneAuthClient";
        const string KEY = "PhoneApiKeyForAuthentication";
        public const int LIFETIME = 30;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

    }
}
