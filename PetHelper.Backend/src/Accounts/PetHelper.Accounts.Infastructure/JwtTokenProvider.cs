using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetHelper.Accounts.Application.Interfaces;
using PetHelper.Accounts.Application.Models;
using PetHelper.Accounts.Domain;
using PetHelper.Accounts.Infastructure.DbContexts;
using PetHelper.Accounts.Infastructure.IdentityManagers;
using PetHelper.Core.Options;

namespace PetHelper.Accounts.Infastructure;

public class JwtTokenProvider : ITokenProvider
{
    private readonly PermissionManager _permissionManager;
    private readonly JwtOptions _jwtOptions;

    public JwtTokenProvider(
        IOptions<JwtOptions> jwtOptions, 
        PermissionManager permissionManager
        )
    {
        _permissionManager = permissionManager;
        _jwtOptions = jwtOptions.Value;
    }
    public async Task<JwtTokenResult> GetAccessToken(
        User user, 
        CancellationToken cancellationToken)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var roleClaims = user.Roles.Select(r => new Claim(ClaimTypes.Role, r.Name ?? string.Empty));

        var permissions = await _permissionManager.GetUserPermissionsCode(user.Id, cancellationToken);
        var permissionsClaims = permissions.Select(r => new Claim(CustomClaims.Permission, r ?? string.Empty));
        
        var jti = Guid.NewGuid();
        
        Claim[] claims = 
        [
            new(CustomClaims.Id, user.Id.ToString()),
            new(CustomClaims.Jti, jti.ToString()),
            new(CustomClaims.Email, user.Email!)
        ];
        
        claims = claims.Union(roleClaims).Union(permissionsClaims).ToArray();
        
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