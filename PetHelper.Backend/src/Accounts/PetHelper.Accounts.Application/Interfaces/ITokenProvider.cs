using PetHelper.Accounts.Application.Models;
using PetHelper.Accounts.Domain;

namespace PetHelper.Accounts.Application.Interfaces;

public interface ITokenProvider
{
    JwtTokenResult GetAccessToken(User user, CancellationToken cancellationToken);
}