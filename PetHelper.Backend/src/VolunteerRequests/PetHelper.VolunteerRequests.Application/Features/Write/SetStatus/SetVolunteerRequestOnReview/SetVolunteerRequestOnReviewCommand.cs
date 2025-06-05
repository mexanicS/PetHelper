using PetHelper.Core.Abstractions.Commands;

namespace PetHelper.VolunteerRequests.Application.Features.Write.SetStatus.SetVolunteerRequestOnReview;
public record SetVolunteerRequestOnReviewCommand(
    Guid VolunteerRequestId,
    Guid AdminId,
    Guid DiscussionId) : ICommand;