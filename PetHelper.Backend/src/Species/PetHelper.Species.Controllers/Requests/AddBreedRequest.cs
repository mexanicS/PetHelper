using PetHelper.Species.Application.SpeciesManagement.Command.AddBreed;

namespace PetHelper.Species.Controllers.Requests;

public record AddBreedRequest(
    AddBreedCommandDto AddBreedCommandDto)
{
    public AddBreedCommand ToCommand(Guid id) =>
        new(id, AddBreedCommandDto);
}

public record AddBreedRequestDto(string Name)
{
    public AddBreedCommandDto ToCommand(Guid id) =>
        new(Name);
}