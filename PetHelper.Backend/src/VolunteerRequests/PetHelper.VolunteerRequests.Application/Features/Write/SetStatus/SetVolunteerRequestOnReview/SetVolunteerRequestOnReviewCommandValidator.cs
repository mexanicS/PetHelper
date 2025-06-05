using FluentValidation;
using PetHelper.Core.Validation;
using PetHelper.SharedKernel.ValueObjects.Discussion;

namespace PetHelper.VolunteerRequests.Application.Features.Write.SetStatus.SetVolunteerRequestOnReview;
public class SetVolunteerRequestOnReviewCommandValidator
    :AbstractValidator<SetVolunteerRequestOnReviewCommand>
{
    public SetVolunteerRequestOnReviewCommandValidator()
    {
        RuleFor(r => r.DiscussionId).MustBeValueObject(DiscussionId.Create);
    }
}
