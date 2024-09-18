using PetHelper.Domain.Shared;

namespace PetHelper.Domain.Models;

public class Species : Entity<SpeciesId>
{
    private Species(SpeciesId id) : base(id)
    { }
    
    private Species(SpeciesId speciesId, string name) : this(speciesId)
    {
        Name = name;    
    }

    public SpeciesId Id { get; set; }
    
    public string Name { get; private set; } = null!;
    
    private readonly List<Breed> _breeds = [];

    public IReadOnlyList<Breed> Breeds => _breeds;
}