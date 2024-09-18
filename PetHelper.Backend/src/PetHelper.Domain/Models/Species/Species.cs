using PetHelper.Domain.Shared;

namespace PetHelper.Domain.Models;

public class Species : Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];
    private Species(SpeciesId id) : base(id)
    { }
    
    private Species(SpeciesId speciesId, string name) : this(speciesId)
    {
        Name = name;    
    }
    
    public string Name { get; private set; } = null!;

    public IReadOnlyList<Breed> Breeds => _breeds;
}