using PetHelper.Application.Volunteers.Commands.ChangeStatusPet;
using PetHelper.Domain.Shared;

namespace PetHelper.API.Controllers.Volunteer.Requests;

public record ChangeStatusPetRequest(
    string Status)
{
    public ChangeStatusPetCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId, petId, Status);
}