using PetHelper.Core.Abstractions.Queries;

namespace PetHelper.Species.Application.SpeciesManagement.Queries.GetBreedsBySpecies;

public record GetBreedsBySpeciesQuery(Guid SpeciesId) : IQuery;