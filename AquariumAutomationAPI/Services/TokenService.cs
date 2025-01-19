using AquariumAutomationAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AquariumAutomationAPI.Services
{
    public class TokenService(IConfiguration _config):ITokenService
    {
        public string CreateToken(User user)
        {
            var _tokenKey = _config["TokenKey"] ?? throw new Exception("Cannot access tokenKey from appsettings");
            if (_tokenKey.Length < 64) throw new Exception("Your token key needs to be longer");
            var _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenKey));

            var _claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserEmail)
            };

            var _creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var _tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(_claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = _creds
            };

            var _tokenHandler = new JwtSecurityTokenHandler();
            var _token = _tokenHandler.CreateToken(_tokenDescriptor);

            return _tokenHandler.WriteToken(_token);

        }
    }
}
