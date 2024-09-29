using PetHelper.Application.DTOs;

namespace PetHelper.Application.Volunteers.UpdateMainInfo;

public record UpdateMainInfoRequest(Guid VolunteerId, 
    UpdateMainInfoDto Dto);
    
public record UpdateMainInfoDto(string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber,
    FullNameDto FullName);