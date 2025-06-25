using FluentValidation;
using PetHelper.Core.Validation;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.VolunteerRequest;

namespace PetHelper.VolunteerRequests.Application.Features.Write.CreateVolunteerRequest;

public class CreateVolunteerRequestCommandValidator : AbstractValidator<CreateVolunteerRequestCommand>
{
    public CreateVolunteerRequestCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        
        RuleFor(r => r.VolunteerInfo).MustBeValueObject(VolunteerInfo.Create);
    }
}