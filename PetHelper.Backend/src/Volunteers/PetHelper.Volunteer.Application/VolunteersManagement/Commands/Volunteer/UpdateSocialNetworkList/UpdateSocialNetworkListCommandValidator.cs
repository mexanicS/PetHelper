using FluentValidation;
using PetHelper.Core.Validation;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.UpdateSocialNetworkList;

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