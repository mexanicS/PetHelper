using PetHelper.Core.Abstractions.Commands;

namespace PetHelper.Species.Application.SpeciesManagement.Command.AddBreed;

public record AddBreedCommand(
    Guid SpeciesId,
    AddBreedCommandDto AddBreedCommandDto) : ICommand;
    
public record AddBreedCommandDto(
    string Name);