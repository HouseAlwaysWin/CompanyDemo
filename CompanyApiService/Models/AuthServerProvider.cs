using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace CompanyApiService.Models
{
    public class AuthServerProvider : OAuthAuthorizationServerProvider
    {


        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var isValidate = new UserValidate().ValidateUser(context.UserName, context.Password);
            if (!isValidate)
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //identity.AddClaim(new Claim(ClaimTypes.Role, "Admin,User"));
            //identity.AddClaim(new Claim(ClaimTypes.Name, "martin"));
            //identity.AddClaim(new Claim("Email", "a@a.com"));
            context.Validated(identity);
        }
    }
}