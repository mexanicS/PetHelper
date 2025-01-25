using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetHelper.Accounts.Application.Interfaces;
using PetHelper.Accounts.Application.Models;
using PetHelper.Accounts.Domain;
using PetHelper.Accounts.Infastructure.DbContexts;
using PetHelper.Core.Options;

namespace PetHelper.Accounts.Infastructure;

public class JwtTokenProvider : ITokenProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly WriteAccountsDbContext _dbContext;

    public JwtTokenProvider(
        IOptions<JwtOptions> jwtOptions, 
        WriteAccountsDbContext dbContext)
    {
        _jwtOptions = jwtOptions.Value;
        _dbContext = dbContext;
    }
    public JwtTokenResult GetAccessToken(
        User user, 
        CancellationToken cancellationToken)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var roleClaims = user.Roles.Select(r => new Claim(ClaimTypes.Role, r.Name ?? string.Empty));

        var jti = Guid.NewGuid();
        
        Claim[] claims = 
        [
            new Claim(CustomClaims.Id, user.Id.ToString()),
            new Claim(CustomClaims.Jti, jti.ToString()),
            new Claim(CustomClaims.Email, user.Email!)
        ];
        
        var jwtToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_jwtOptions.ExpirationTime)),
            signingCredentials: signingCredentials,
            claims: claims
        );
        
        var token =   new JwtSecurityTokenHandler().WriteToken(jwtToken);
        
        return new JwtTokenResult(token, jti);
    }
}