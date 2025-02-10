namespace PetHelper.Core.DTOs.ReadDtos;

public class VolunteerDto
{
    public Guid Id { get; init; }
    
    //public string Name { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;
        
    public string Description { get; init; } = string.Empty;

    public int ExperienceInYears { get; init; } = 0;

    public string PhoneNumber { get; init; } = string.Empty;
    
    public PetDto[] Pets { get; init; } = [];

    public SocialNetworkDto[] SocialNetwork { get; init; } = [];

    public DetailsForAssistanceDto[] DetailsForAssistance { get; init; } = [];
}