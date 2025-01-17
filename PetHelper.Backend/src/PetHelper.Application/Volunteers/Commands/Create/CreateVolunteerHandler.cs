using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelper.Application.Abstractions;
using PetHelper.Application.Abstractions.Commands;
using PetHelper.Domain.Models.Volunteer;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;
using PetHelper.Domain.ValueObjects.Common;

namespace PetHelper.Application.Volunteers.Commands.Create;

public class CreateVolunteerHandler : ICommandHandler<Guid,CreateVolunteerCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    
    private readonly ILogger _logger;
    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<CreateVolunteerHandler> logger) 
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid,ErrorList>> Handle(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var volunteer = CreateVolunteer(command);
        if (volunteer.IsFailure)
            return volunteer.Error.ToErrorList();
        
        await _volunteersRepository.Add(volunteer.Value, cancellationToken);
        _logger.LogInformation("Created volunteer added with id {volunteerId}", volunteer.Value.Id.Value);
        
        return volunteer.Value.Id.Value;
    }
    private Result<Volunteer, Error> CreateVolunteer(CreateVolunteerCommand command)
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

        return new Volunteer(
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