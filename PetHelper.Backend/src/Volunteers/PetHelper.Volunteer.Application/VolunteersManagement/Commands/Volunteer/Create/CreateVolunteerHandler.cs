using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetHelper.Core;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects;
using PetHelper.SharedKernel.ValueObjects.Common;
using PetHelper.Volunteer.Domain.Ids;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.Create;

public class CreateVolunteerHandler : ICommandHandler<Guid,CreateVolunteerCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<CreateVolunteerHandler> logger,
        [FromKeyedServices(Constants.Context.VolunteerManagement)] IUnitOfWork unitOfWork) 
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid,ErrorList>> Handle(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var volunteer = CreateVolunteer(command);
        if (volunteer.IsFailure)
            return volunteer.Error.ToErrorList();
        
        await _volunteersRepository.AddAsync(volunteer.Value, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Created volunteer added with id {volunteerId}", volunteer.Value.Id.Value);
        
        return volunteer.Value.Id.Value;
    }
    private Result<Domain.Volunteer, Error> CreateVolunteer(CreateVolunteerCommand command)
    {
        var id = VolunteerId.NewId();
        var fullNameRequest = FullName.Create(
            command.FullName.FirstName, 
            command.FullName.LastName, 
            command.FullName.MiddleName).Value;
        
        var email = Email.Create(command.Email).Value;
        
        var description = Description.Create(command.Description).Value;
        
        var experience = ExperienceInYears.Create(command.ExperienceInYears).Value;
        
        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
        
        var socialNetwork = new SocialNetworkList(
            command.SocialNetworks.SocialNetworks
                .Select(c => SocialNetwork.Create(c.Name, c.Url).Value)
        );
        
        var detailsForAssistance = new DetailsForAssistanceList(
            command.DetailsForAssistances.DetailsForAssistances
                .Select(c => DetailsForAssistance.Create(c.Name, c.Description).Value)
        );

        return new Domain.Volunteer(
            id,
            fullNameRequest,
            email,
            description,
            experience,
            phoneNumber,
            socialNetwork,
            detailsForAssistance
        );
    }
}