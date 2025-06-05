using FluentValidation;
using PetHelper.Core.Validation;
using PetHelper.SharedKernel.ValueObjects.VolunteerRequest;

namespace PetHelper.VolunteerRequests.Application.Features.Write.SetStatus.SetVolunteerRequestRejected;
public class SetVolunteerRequestRejectedCommandValidator
    :AbstractValidator<SetVolunteerRequestRejectedCommand>
{
    public SetVolunteerRequestRejectedCommandValidator()
    {
        RuleFor(r => r.RejectedComment).MustBeValueObject(RequestComment.Create);
    }
}
