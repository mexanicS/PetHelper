using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.Common;
using PetHelper.SharedKernel.ValueObjects.Discussion;
using PetHelper.SharedKernel.ValueObjects.User;
using PetHelper.SharedKernel.ValueObjects.VolunteerRequest;

namespace PetHelper.VolunteerRequests.Domain;

public class VolunteerRequest
{
    public VolunteerRequestId Id { get; private set; }
    public UserId? AdminId { get; private set; }
    public UserId UserId { get; private set; }
    public VolunteerInfo? VolunteerInfo { get; private set; }
    public Constants.VolunteerRequestStatus? Status { get; private set; }
    public Date CreatedAt { get; private set; }
    public RequestComment RejectedComment { get; private set; }
    public DiscussionId? DiscussionId { get; private set; }

    public VolunteerRequest(
        UserId userId,
        VolunteerInfo? volunteerInfo)
    {
        Id = VolunteerRequestId.Create().Value;
        UserId = userId;
        CreatedAt = Date.Create().Value;
        Status = Constants.VolunteerRequestStatus.Submitted;
        VolunteerInfo = volunteerInfo;
    }

    public static VolunteerRequest Create(
        UserId userId,
        VolunteerInfo? volunteerInfo)
    {
        return new VolunteerRequest(userId, volunteerInfo);
    }

    public void SetOnReview(
        UserId adminId,
        DiscussionId? discussionId)
    {
        AdminId = adminId;
        Status = Constants.VolunteerRequestStatus.OnReview;
        DiscussionId = discussionId;
    }

    public void SetRevisionRequired(
        UserId adminId,
        RequestComment rejectedComment)
    {
        AdminId = adminId;
        Status = Constants.VolunteerRequestStatus.RevisionRequired;
        RejectedComment = rejectedComment;
    }

    public void SetRejected(
        UserId adminId,
        RequestComment rejectedComment)
    {
        AdminId = adminId;
        Status = Constants.VolunteerRequestStatus.Rejected;
        RejectedComment = rejectedComment;
    }

    public void SetApproved(
        UserId adminId)
    {
        AdminId = adminId;
        Status = Constants.VolunteerRequestStatus.Approved;
    }

    public void SetSubmitted(
        UserId adminId)
    {
        AdminId = adminId;
        Status = Constants.VolunteerRequestStatus.Submitted;
    } 
    
}