using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetHelper.Core;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.User;
using PetHelper.VolunteerRequests.Application.DataBase.Interfaces;
using PetHelper.VolunteerRequests.Domain;

namespace PetHelper.VolunteerRequests.Application.Features.Write.SetStatus.SetVolunteerRequestSubmitted;

public class SetVolunteerRequestSubmittedHandler
    : BaseVolunteerRequestStatusHandler<SetVolunteerRequestSubmittedCommand>
{
    public SetVolunteerRequestSubmittedHandler(
        IVolunteerRequestRepository repository,
        [FromKeyedServices(Constants.VOLUNTEER_REQUEST_UNIT_OF_WORK_KEY)] IUnitOfWork unitOfWork,
        ILogger<SetVolunteerRequestSubmittedHandler> logger)
        : base(repository, unitOfWork, logger) { }

    protected override void UpdateStatus(VolunteerRequest volunteerRequest, UserId adminId)
        => volunteerRequest.SetSubmitted(adminId);
}