using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Domain.Models;

public class Breed : Entity<BreedId>
{
    private Breed(BreedId id) : base(id)
    { }

    public Breed(BreedId breedId, Name name) : this(breedId)
    {
        Name = name;    
    }
    
    public Name Name { get; private set; }
}