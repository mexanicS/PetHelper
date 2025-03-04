using CSharpFunctionalExtensions;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.Common;
using PetHelper.SharedKernel.ValueObjects.ModelIds;

namespace PetHelper.Species.Domain.Models;

public class Breed : Entity
{
    private Breed(BreedId id)
    { }

    public Breed(BreedId breedId,
        Name name) : this(breedId)
    {
        Id = breedId;
        Name = name;    
    }
    
    public new BreedId Id { get; private set; }
    
    public Name Name { get; private set; } = default!;
}