using System.Text;
using Microsoft.IdentityModel.Tokens;
using PetHelper.Core.Options;

namespace PetHelper.Framework.Authorization;

public static class TokenValidationParametersFactory
{
    public static TokenValidationParameters Create(JwtOptions jwtOptions, bool validateLifeTime = true)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = validateLifeTime,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
            ClockSkew = TimeSpan.Zero
        };
    }
}