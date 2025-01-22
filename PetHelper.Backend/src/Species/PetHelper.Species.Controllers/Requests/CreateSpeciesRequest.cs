using PetHelper.Species.Application.SpeciesManagement.Command.Create;

namespace PetHelper.Species.Controllers.Requests;

public record CreateSpeciesRequest(string Name)
{
    public CreateSpeciesCommand ToCommand() =>
        new (Name);
}