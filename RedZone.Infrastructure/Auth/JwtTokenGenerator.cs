using RedZone.App.Common.Interfaces.Auth;
using RedZone.App.Common.Interfaces.Services;
using RedZone.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace RedZone.Infrastructure.Auth
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IDateTimeProvider _dateTimeProvider;

        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettings)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var InputClaims = new ClaimsIdentity();
            foreach (var claim in claims)
            {
                InputClaims.AddClaim(claim);
            }

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = InputClaims,
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = signinCredentials

            };
            var token = jwtTokenHandler.CreateToken(tokenDes);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }

        public string GenerateToken(User user)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256
                );

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName,user.Name),
                new Claim(JwtRegisteredClaimNames.FamilyName,user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                
            };
            var securityToken = new JwtSecurityToken(claims: claims
                , signingCredentials: signingCredentials, issuer: _jwtSettings.Issuer,
                audience:_jwtSettings.Audience,
                expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.Expire)
                ) ;
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public ClaimsPrincipal GetInfoJwtToken(string JwtToken)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey,
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(JwtToken, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }

        public string RefreshTokenGeneration()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
