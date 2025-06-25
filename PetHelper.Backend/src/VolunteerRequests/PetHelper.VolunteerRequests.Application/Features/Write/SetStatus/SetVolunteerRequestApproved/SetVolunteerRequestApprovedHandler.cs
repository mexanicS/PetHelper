using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetHelper.Core;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.User;
using PetHelper.VolunteerRequests.Application.DataBase.Interfaces;
using PetHelper.VolunteerRequests.Application.Features.Write.SetStatus.SetVolunteerRequestSubmitted;
using PetHelper.VolunteerRequests.Domain;

namespace PetHelper.VolunteerRequests.Application.Features.Write.SetStatus.SetVolunteerRequestApproved;

public class SetVolunteerRequestApprovedHandler
    : BaseVolunteerRequestStatusHandler<SetVolunteerRequestApprovedCommand>
{
    public SetVolunteerRequestApprovedHandler(
        IVolunteerRequestRepository volunteerRequestRepository,
        [FromKeyedServices(Constants.Context.VolunteersRequest)] IUnitOfWork unitOfWork,
        ILogger<SetVolunteerRequestApprovedHandler> logger)
        : base(volunteerRequestRepository, unitOfWork, logger) { }

    protected override void UpdateStatus(VolunteerRequest volunteerRequest, UserId adminId)
        => volunteerRequest.SetApproved(adminId);
}