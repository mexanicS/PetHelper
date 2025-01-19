using PetHelper.Application.Abstractions.Commands;

namespace PetHelper.Application.Species.AddBreed;

public record AddBreedCommand(
    Guid SpeciesId,
    AddBreedCommandDto AddBreedCommandDto) : ICommand;
    
public record AddBreedCommandDto(
    string Name);