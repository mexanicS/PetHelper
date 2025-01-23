using PetHelper.Core.DTOs;
using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Pet.UpdatePet;

namespace PetHelper.Volunteer.Controllers.Requests.Volunteer;

public record UpdatePetRequest(
    Guid SpeciesId,
    Guid BreedId,
    string Name,
    string TypePet,
    string Description,
    string Color,
    string HealthInformation,
    double Weight,
    double Height,
    string PhoneNumber,
    bool IsNeutered,
    DateOnly? BirthDate,
    bool IsVaccinated,
    string City,
    string Street,
    string HouseNumber,
    string? ZipCode,
    DetailsForAssistanceListDto DetailsForAssistances)
{
    public UpdatePetCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId,
            petId,
            SpeciesId,
            BreedId,
            Name,
            TypePet,
            Description,
            Color,
            HealthInformation,
            Weight,
            Height,
            PhoneNumber,
            IsNeutered,
            BirthDate,
            IsVaccinated,
            City,
            Street,
            HouseNumber,
            ZipCode,
            DetailsForAssistances);
}