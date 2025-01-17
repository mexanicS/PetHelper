using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;
using PetHelper.Domain.ValueObjects.Common;

namespace PetHelper.Domain.Models.Breed;

public class Breed : Entity<BreedId>
{
    private Breed(BreedId id) : base(id)
    { }

    public Breed(BreedId breedId, Name name) : this(breedId)
    {
        Name = name;    
    }
    
    public Name Name { get; private set; } = default!;
}