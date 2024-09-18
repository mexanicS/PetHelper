using PetHelper.Domain.Shared;

namespace PetHelper.Domain.Models;

public class Breed : Entity<BreedId>
{
    private Breed(BreedId id) : base(id)
    { }

    private Breed(BreedId breedId, string name) : this(breedId)
    {
        Name = name;    
    }
    
    public string Name { get; private set; }
}