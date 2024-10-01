using FluentValidation;
using PetHelper.Application.Validation;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Volunteers.UpdateSocialNetworkList;

public class UpdateSocialNetworkListRequestValidator : AbstractValidator<UpdateSocialNetworkListRequest>
{
    public UpdateSocialNetworkListRequestValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class UpdateSocialNetworkListRequestDtoValidator : AbstractValidator<UpdateSocialNetworkListRequestDto>
{
    public UpdateSocialNetworkListRequestDtoValidator()
    {
        RuleForEach(request=>request.SocialNetworkListDto.SocialNetworks)
            .MustBeValueObject(x => 
                SocialNetwork.Create(x.Name, x.Url));
    }
}