using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetHelper.Core;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.User;
using PetHelper.SharedKernel.ValueObjects.VolunteerRequest;
using PetHelper.VolunteerRequests.Application.DataBase.Interfaces;
using PetHelper.VolunteerRequests.Domain;

namespace PetHelper.VolunteerRequests.Application.Features.Write.CreateVolunteerRequest;

public class CreateVolunteerRequestHandler : ICommandHandler<CreateVolunteerRequestCommand>
{
    private readonly IVolunteerRequestRepository _volunteerRequestRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateVolunteerRequestHandler> _logger;

    public CreateVolunteerRequestHandler(
        IVolunteerRequestRepository volunteerRequestRepository,
        [FromKeyedServices(Constants.VOLUNTEER_REQUEST_UNIT_OF_WORK_KEY)] IUnitOfWork unitOfWork,
        ILogger<CreateVolunteerRequestHandler> logger)
    {
        _volunteerRequestRepository = volunteerRequestRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>> Handle(CreateVolunteerRequestCommand command,
        CancellationToken cancellationToken)
    {
        var userId = UserId.Create(command.UserId).Value;
        var volunteerInfo = VolunteerInfo.Create(command.VolunteerInfo).Value;
        var volunteerRequest = VolunteerRequest.Create(userId, volunteerInfo);
        
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
        
        await _volunteerRequestRepository.Add(volunteerRequest);
        transaction.Commit();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("VolunteerRequest created by {UserId}", userId);
        
        return Result.Success<ErrorList>();
    }
}