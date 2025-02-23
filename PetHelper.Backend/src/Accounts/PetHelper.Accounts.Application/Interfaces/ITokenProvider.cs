using System.Security.Claims;
using CSharpFunctionalExtensions;
using PetHelper.Accounts.Application.Models;
using PetHelper.Accounts.Domain;
using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Application.Interfaces;

public interface ITokenProvider
{
    Task<JwtTokenResult> GetAccessToken(User user, CancellationToken cancellationToken);
    
    Task<Guid> GenerateRefreshToken(User user, Guid accessTokenJti, CancellationToken cancellationToken);

    Task<Result<IReadOnlyList<Claim>, Error>> GetUserClaims(string jwtToken, CancellationToken cancellationToken);
}