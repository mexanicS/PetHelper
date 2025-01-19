using PetHelper.Application.Species.Create;

namespace PetHelper.API.Controllers.Species.Requests;

public record CreateSpeciesRequest(string Name)
{
    public CreateSpeciesCommand ToCommand() =>
        new (Name);
}