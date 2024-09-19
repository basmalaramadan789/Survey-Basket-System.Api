using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.FileIO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyBasket.Api.Authentication
{
    public class JwtProvider(IOptions<JwtOptions> option) : IJwtProvider
    {
        private readonly IOptions<JwtOptions> _option = option;

        public (string token, int ExpiresIn) GenerateToken(ApplicationUser user,IEnumerable<string> rols,IEnumerable<string> permissions)
        {
            Claim[] claims = [
                new(JwtRegisteredClaimNames.Sub,user.Id),
                new(JwtRegisteredClaimNames.Email,user.Email!),
                new(JwtRegisteredClaimNames.GivenName,user.FirstName),
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new(nameof(rols),string.Join(',',rols)),
                new(nameof(permissions),string.Join(',',permissions))


            ];
            var symetricSecKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_option.Value.Key));

            var sighningCredentail = new SigningCredentials(symetricSecKey, SecurityAlgorithms.HmacSha256);

            
            var expirationDate= DateTime.UtcNow.AddMinutes(_option.Value.ExpireMinutes);

            var token = new JwtSecurityToken(
                issuer:_option.Value.Issuer,
                audience: _option.Value.Audience,
                claims:claims,
                expires: expirationDate,
                signingCredentials: sighningCredentail


            );
            return (token: new JwtSecurityTokenHandler().WriteToken(token), ExpiresIn: _option.Value.ExpireMinutes * 60);

        }
        // Refresh Token
        // validate token if valid => userId else Return Null
        public string? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var symetricSecKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_option.Value.Key));

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = symetricSecKey,
                    ValidateIssuerSigningKey=true,
                    ValidateIssuer =false,
                    ValidateAudience=false,
                    ClockSkew=TimeSpan.Zero

                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                return jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value; 

            }
            catch
            {
                return null;
            }

        }
    }
}
