namespace PetHelper.Application.DTOs;

public record VolunteerDetailsDto(
    SocialNetworkListDto SocialNetworks,
    DetailsForAssistanceListDto DetailsForAssistances);