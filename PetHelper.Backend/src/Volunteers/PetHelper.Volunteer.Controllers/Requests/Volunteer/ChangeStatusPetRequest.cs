using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Pet.ChangeStatusPet;

namespace PetHelper.Volunteer.Controllers.Requests.Volunteer;

public record ChangeStatusPetRequest(
    string Status)
{
    public ChangeStatusPetCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId, petId, Status);
}