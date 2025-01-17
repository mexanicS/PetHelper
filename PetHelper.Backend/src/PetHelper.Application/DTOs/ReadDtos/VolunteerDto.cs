namespace PetHelper.Application.DTOs.ReadDtos;

public class VolunteerDto
{
    public Guid Id { get; init; }
    
    //public string Name { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;
        
    public string Description { get; init; } = string.Empty;

    public int ExperienceInYears { get; init; } = 0;

    public string PhoneNumber { get; init; } = string.Empty;
    
    public PetDto[] Pets { get; init; } = [];  
    
    //public SocialNetworkList SocialNetwork { get; private set; }

    //public DetailsForAssistanceList DetailsForAssistance { get; private set; } 
}