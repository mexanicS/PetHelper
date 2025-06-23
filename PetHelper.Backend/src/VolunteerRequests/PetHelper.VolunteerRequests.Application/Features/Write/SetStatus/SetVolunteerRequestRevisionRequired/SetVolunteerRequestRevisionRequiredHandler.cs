using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetHelper.Core;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.User;
using PetHelper.SharedKernel.ValueObjects.VolunteerRequest;
using PetHelper.VolunteerRequests.Application.DataBase.Interfaces;

namespace PetHelper.VolunteerRequests.Application.Features.Write.SetStatus.SetVolunteerRequestRevisionRequired;
public class SetVolunteerRequestRevisionRequiredHandler
    : ICommandHandler<SetVolunteerRequestRevisionRequiredCommand>
{

    private readonly IVolunteerRequestRepository _volunteerRequestRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SetVolunteerRequestRevisionRequiredHandler> _logger;

    public SetVolunteerRequestRevisionRequiredHandler(
        IVolunteerRequestRepository volunteerRequestRepository,
        [FromKeyedServices(Constants.Context.VolunteersRequest)] IUnitOfWork unitOfWork,
        ILogger<SetVolunteerRequestRevisionRequiredHandler> logger)
    {
        _volunteerRequestRepository = volunteerRequestRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        SetVolunteerRequestRevisionRequiredCommand command, CancellationToken ct)
    {
        var volunteerRequest = await _volunteerRequestRepository.GetById(command.VolunteerRequestId, ct);
        if (volunteerRequest is null)
            return Error.NotFound("VolunteerRequest.not.found", 
                $"VolunteerRequest with id = {command.VolunteerRequestId} not found").ToErrorList();
        
        var oldStatus = volunteerRequest.Status;
        var adminId = UserId.Create(command.AdminId).Value;
        var comment = RequestComment.Create(command.RejectedComment).Value;
        volunteerRequest.SetRejected(adminId, comment);

        var transaction = await _unitOfWork.BeginTransaction(ct);
        _volunteerRequestRepository.Update(volunteerRequest);
        transaction.Commit();
        await _unitOfWork.SaveChangesAsync(ct);
        
        return Result.Success<ErrorList>();
    }
}
