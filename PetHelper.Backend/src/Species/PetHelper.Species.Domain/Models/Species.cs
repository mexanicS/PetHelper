
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.Common;
using PetHelper.SharedKernel.ValueObjects.ModelIds;

namespace PetHelper.Species.Domain.Models
{
    public class Species : Entity<SpeciesId>
    {
        private readonly List<Breed> _breeds = [];
        private Species(SpeciesId id) : base(id)
        { }
    
        public Species(SpeciesId speciesId, Name name) : this(speciesId)
        {
            Name = name;    
        }
    
        public Name Name { get; private set; } = null!;

        public IReadOnlyList<Breed> Breeds => _breeds;

        public void AddBreed(Breed breed)
        {
            _breeds.Add(breed);
        }

        public void RemoveBreed(Breed breed)
        {
            _breeds.Remove(breed);
        }
    }
}

