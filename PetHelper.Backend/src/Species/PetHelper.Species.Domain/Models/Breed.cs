using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.Common;
using PetHelper.SharedKernel.ValueObjects.ModelIds;

namespace PetHelper.Species.Domain.Models;

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