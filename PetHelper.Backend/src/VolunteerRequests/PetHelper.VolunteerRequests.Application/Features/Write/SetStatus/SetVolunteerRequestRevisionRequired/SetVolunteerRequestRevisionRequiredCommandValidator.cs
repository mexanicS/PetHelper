using FluentValidation;
using PetHelper.Core.Validation;
using PetHelper.SharedKernel.ValueObjects.VolunteerRequest;

namespace PetHelper.VolunteerRequests.Application.Features.Write.SetStatus.SetVolunteerRequestRevisionRequired;
public class SetVolunteerRequestRevisionRequiredCommandValidator
    : AbstractValidator<SetVolunteerRequestRevisionRequiredCommand>
{
    public SetVolunteerRequestRevisionRequiredCommandValidator()
    {
        RuleFor(r => r.RejectedComment).MustBeValueObject(RequestComment.Create);
    }
}
