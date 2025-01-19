using FluentValidation;
using PetHelper.Application.Validation;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Volunteers.Commands.UpdateSocialNetworkList;

public class UpdateSocialNetworkListCommandValidator : AbstractValidator<UpdateSocialNetworkListCommand>
{
    public UpdateSocialNetworkListCommandValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class UpdateSocialNetworkListCommandDtoValidator : AbstractValidator<UpdateSocialNetworkListCommandDto>
{
    public UpdateSocialNetworkListCommandDtoValidator()
    {
        RuleForEach(request=>request.SocialNetworkListDto.SocialNetworks)
            .MustBeValueObject(x => 
                SocialNetwork.Create(x.Name, x.Url));
    }
}