using PetHelper.Application.Abstractions.Commands;

namespace PetHelper.Application.Species.Command.AddBreed;

public record AddBreedCommand(
    Guid SpeciesId,
    AddBreedCommandDto AddBreedCommandDto) : ICommand;
    
public record AddBreedCommandDto(
    string Name);