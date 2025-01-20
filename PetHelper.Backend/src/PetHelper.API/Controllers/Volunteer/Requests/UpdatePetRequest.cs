using PetHelper.Application.DTOs;
using PetHelper.Application.Volunteers.Commands.AddPet;
using PetHelper.Application.Volunteers.Commands.UpdatePetCommand;

namespace PetHelper.API.Controllers.Volunteer.Requests;

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