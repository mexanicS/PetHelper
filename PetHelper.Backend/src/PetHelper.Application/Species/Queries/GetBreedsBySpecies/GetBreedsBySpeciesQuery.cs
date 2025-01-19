using PetHelper.Application.Abstractions.Queries;

namespace PetHelper.Application.Species.Queries.GetBreedsBySpecies;

public record GetBreedsBySpeciesQuery(Guid SpeciesId) : IQuery;