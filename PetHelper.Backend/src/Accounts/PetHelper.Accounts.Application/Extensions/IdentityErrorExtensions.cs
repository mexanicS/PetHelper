using Microsoft.AspNetCore.Identity;
using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Application.Extensions;


public static class IdentityErrorExtensions
{
    public static ErrorList ToErrorList(this IEnumerable<IdentityError> errors)
    {
        return new ErrorList(errors.Select(x=> Error.Failure(x.Code, x.Description)));
    }
}