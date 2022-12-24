using Core.Utilities.Current;
using Entities.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class TokenService : ITokenService
    {
        IConfiguration Configuration;

        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            Token tokenInstance = new Token();

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            tokenInstance.Expiration = DateTime.Now.AddMinutes(5);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: tokenInstance.Expiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user),
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            tokenInstance.AccessToken = tokenHandler.WriteToken(securityToken);
            tokenInstance.RefreshToken = CreateRefreshToken();
            return tokenInstance;

        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }

        private IEnumerable<Claim> SetClaims(User user)
        {
            //NetaBlueDBContext context = new NetaBlueDBContext();
            //var companyPayment = context.CompanyPayments.Where(x => x.CompanyId == member.CompanyId && x.IsActive == true).FirstOrDefault();
            //var companyLicence = context.CompanyLicences.Where(x => x.CompanyId == member.CompanyId && x.IsActive == true).FirstOrDefault();
            //var getLicenceDay = companyPayment.IsTrial == true ? (companyPayment.TrialExpiredDate - DateTime.Now)?.Days : companyPayment.IsPaid == true ? (companyLicence.ExpiredDate - DateTime.Now).Days : 0;
            //bool getIsManager = context.Companies.Any(x => x.ManagerId == member.Id && x.IsActive == true);

            var claims = new List<Claim>();
            //claims.AddNameIdentifier(user.Id.ToString());
            //claims.AddEmail(user.EmailAddress);
            //claims.AddName($"{user.FirstName} {user.LastName}");
            //var roles = operationPermission.Select(c => c.OperationName).Count() == 0 ? new string[] { "" } : operationPermission.Select(c => c.OperationName).Distinct().ToArray();
            //claims.AddRoles(roles);
            claims.AddRange(new Claim[]
                            {
                     //new Claim(ClaimTypes.NameIdentifier,member.Id.ToString()),
                     new Claim(ClaimTypes.Email,user.EmailAddress),
                     //new Claim(ClaimTypes.Name,user.FirstName),
                     //new Claim(ClaimTypes.Surname,user.LastName),
                     //new Claim("PhoneNumber",member.PhoneNumber == null ?  "" : member.PhoneNumber) ,
                     //new Claim("PhotoURL",member.PhotoUrl == null ?  "" : member.PhotoUrl),
                     //new Claim("MemberId",member.Id.ToString()),
                     //new Claim("CompanyId",member.CompanyId.ToString()),
                     //new Claim("IsPaid",companyPayment.IsPaid.ToString()),
                     //new Claim("IsTrial",companyPayment.IsTrial.ToString()),
                     //new Claim("RemainingLicenceDay",getLicenceDay.ToString()),
                     //new Claim("IsManager",getIsManager.ToString()),
                     //new Claim("Enable2FA",member.Enable2Fa == true ? "1" : "0"),

                            }); ;
            return claims;
        }

    }
}
