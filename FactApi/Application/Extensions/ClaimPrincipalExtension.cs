using System.Security.Claims;
using FactApi.Domain.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace FactApi.Application.Extensions
{
    public static class ClaimPrincipalExtension
    {
        const string ClaimCompanyId = "CompanyId";
        public static ClaimsIdentity GetClaimsIdentity(this AuthResponseModel m, string authenticationScheme = JwtBearerDefaults.AuthenticationScheme)
        {
            var claims = new List<Claim>
            {            
                new Claim(ClaimTypes.Name, m.Username),
                new Claim(ClaimTypes.Email, m.Email),
                new Claim(ClaimCompanyId, m.CompanyId.ToString()),

            };

            var claimsIdentity = new ClaimsIdentity(
            claims, authenticationScheme);

            return claimsIdentity;
        }

        internal static AuthCurrentModel GetUser(this ControllerBase controller)
        {
            var usr = new AuthCurrentModel();
            var identity = controller.User.Identity as ClaimsIdentity;
            foreach (var claim in identity.Claims)
            {
                switch (claim.Type)
                {
                    case ClaimTypes.NameIdentifier:
                        usr.Username = claim.Value; break;
                    case ClaimTypes.Email:
                        usr.Email = claim.Value; break;
                    case ClaimCompanyId:
                        usr.CompanyId = int.Parse(claim.Value); break;
                    case ClaimTypes.Name:
                        usr.Username = claim.Value; break;
                }
            }
            return usr;
        }
    }
}