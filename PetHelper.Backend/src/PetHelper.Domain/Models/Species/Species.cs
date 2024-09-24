using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Domain.Models;

public class Species : Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];
    private Species(SpeciesId id) : base(id)
    { }
    
    private Species(SpeciesId speciesId, Name name) : this(speciesId)
    {
        Name = name;    
    }
    
    public Name Name { get; private set; } = null!;

    public IReadOnlyList<Breed> Breeds => _breeds;
}