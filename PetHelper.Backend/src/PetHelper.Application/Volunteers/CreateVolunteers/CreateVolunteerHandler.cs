using PetHelper.Application.DTOs;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Volunteers.CreateVolunteers;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    
    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateVolunteerRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var volunteer = CreateVolunteer(request);
        
        await _volunteersRepository.Add(volunteer, cancellationToken);
        
        return volunteer.Id.Value;
    }
    private Volunteer CreateVolunteer(CreateVolunteerRequest request)
    {
        var id = VolunteerId.NewId();
        var fullName = FullName.Create(request.FullName.FirstName, request.FullName.LastName, request.FullName.MiddleName);
        var email = Email.Create(request.Email);
        var description = Description.Create(request.Description).Value;
        var experience = ExperienceInYears.Create(request.ExperienceInYears);
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber);
        
        var socialNetwork = new SocialNetworkList(
            request.SocialNetworks.SocialNetworks
                .Select(c => SocialNetwork.Create(c.Name, c.Url))
        );
        
        var detailsForAssistance = new DetailsForAssistanceList(
            request.DetailsForAssistances.DetailsForAssistances
                .Select(c => DetailsForAssistance.Create(c.Name, c.Description).Value)
        );

        return new Volunteer(
            id,
            fullName,
            email,
            description,
            experience,
            phoneNumber,
            socialNetwork,
            detailsForAssistance
        );
    }
}