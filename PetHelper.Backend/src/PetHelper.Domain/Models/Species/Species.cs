using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;
using PetHelper.Domain.ValueObjects.Common;

namespace PetHelper.Domain.Models.Species
{
    public class Species : Entity<SpeciesId>
    {
        private readonly List<Breed.Breed> _breeds = [];
        private Species(SpeciesId id) : base(id)
        { }
    
        public Species(SpeciesId speciesId, Name name) : this(speciesId)
        {
            Name = name;    
        }
    
        public Name Name { get; private set; } = null!;

        public IReadOnlyList<Breed.Breed> Breeds => _breeds;

        public void AddBreed(Breed.Breed breed)
        {
            _breeds.Add(breed);
        }
    }
}

