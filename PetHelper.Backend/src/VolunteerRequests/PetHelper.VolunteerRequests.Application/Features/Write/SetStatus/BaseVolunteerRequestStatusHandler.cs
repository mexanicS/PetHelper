using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetHelper.Core;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.User;
using PetHelper.VolunteerRequests.Application.DataBase.Interfaces;
using PetHelper.VolunteerRequests.Domain;

namespace PetHelper.VolunteerRequests.Application.Features.Write.SetStatus;

public abstract class BaseVolunteerRequestStatusHandler<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand, IVolunteerRequestStatusCommand
{
    private readonly IVolunteerRequestRepository _volunteerRequestRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BaseVolunteerRequestStatusHandler<TCommand>> _logger;

    protected BaseVolunteerRequestStatusHandler(
        IVolunteerRequestRepository volunteerRequestRepository,
        [FromKeyedServices(Constants.Context.VolunteersRequest)] IUnitOfWork unitOfWork,
        ILogger<BaseVolunteerRequestStatusHandler<TCommand>> logger)
    {
        _volunteerRequestRepository = volunteerRequestRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>> Handle(TCommand command, CancellationToken ct)
    {
        var volunteerRequest = await _volunteerRequestRepository.GetById(command.VolunteerRequestId, ct);
        if (volunteerRequest is null)
            return Error.NotFound("VolunteerRequest.not.found", 
                $"VolunteerRequest with id = {command.VolunteerRequestId} not found").ToErrorList();
        var oldStatus = volunteerRequest.Status;
        var adminId = UserId.Create(command.AdminId).Value;

        UpdateStatus(volunteerRequest, adminId);

        var transaction = await _unitOfWork.BeginTransaction(ct);
        _volunteerRequestRepository.Update(volunteerRequest);
        transaction.Commit();
        await _unitOfWork.SaveChangesAsync(ct);
        
        _logger.LogInformation("VolunteerRequest with id = {VolunteerRequestId} status changed from {oldStatus} to {newStatus}", 
            volunteerRequest.Id, oldStatus.ToString(), volunteerRequest.Status.ToString());
        
        return Result.Success<ErrorList>();
    }

    protected abstract void UpdateStatus(VolunteerRequest volunteerRequest, UserId adminId);
}