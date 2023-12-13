using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JsonWebTokenExample.Configs;
using JsonWebTokenExample.WebAPI.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JsonWebTokenExample.WebAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly JWTConfig _jwtConfig;
        public AuthenticationService(ILogger<AuthenticationService> logger,
                                     IOptions<JWTConfig> jwtConfig)
        {
            _logger = logger;
            _jwtConfig = jwtConfig.Value;
        }
        public async Task<string> IssueTokenAsync(string userName)
        {
            try
            {
                ClaimsIdentity subject = new(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, userName)
                });

                DateTime expiryTime = DateTime.UtcNow.AddSeconds(_jwtConfig.DurationSeconds);
                SigningCredentials signingCredentials = new(_jwtConfig.IssuerSigningKey, SecurityAlgorithms.HmacSha256Signature);
                SecurityTokenDescriptor tokenDescriptor = new()
                {
                    Subject = subject,
                    Expires = expiryTime,
                    Issuer = _jwtConfig.Issuer,
                    Audience = _jwtConfig.Audience,
                    SigningCredentials = signingCredentials
                };

                JwtSecurityTokenHandler tokenHandler = new();
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"EXCEPTION in {nameof(IssueTokenAsync)}");
                throw;
            }
        }
    }
}