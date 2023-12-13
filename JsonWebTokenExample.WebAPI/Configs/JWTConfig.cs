using System.Text;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;

namespace JsonWebTokenExample.Configs
{
    public class JWTConfig
    {
        public const string ConfigBinding = "JWT";

        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        [JsonIgnore]
        public SecurityKey IssuerSigningKey
        {
            get
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(Key);
                return new SymmetricSecurityKey(keyBytes);
            }
        }
        public long DurationSeconds { get; set; }
    }
}