using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Domain.Models
{
    public class Pet : Entity<PetId>
    {
        //ef core
        private Pet(PetId id) : base(id)
        { }

        private Pet(PetId petId,
            Name name, 
            TypePet typePet, 
            Description description,
            ValueObjects.Breed breed,
            Color color,
            HealthInformation healthInformation,
            double weight,
            double height,
            PhoneNumber phoneNumber,
            bool isNeutered,
            DateOnly birthDate,
            bool isVaccinated,
            DateTime createdDate
                )
            : base(petId)
        {
            Id = petId;
            Name = name;
            TypePet = typePet;
            Description = description;
            Breed = breed;
            Color = color;
            HealthInformation = healthInformation;
            Weight = weight;
            Height = height;
            PhoneNumber = phoneNumber;
            IsNeutered = isNeutered;
            DateOfBirth = birthDate;
            IsVaccinated = isVaccinated;
            CreatedDate = createdDate;
        }

        public PetId Id { get; private set; }

        public Name Name { get; private set; } = null!;

        public TypePet TypePet { get; private set; } = null!;

        public Description Description { get; private set; } = null!;

        public ValueObjects.Breed Breed { get; private set; } = null!;

        public Color Color { get; private set; } = null!;

        public HealthInformation HealthInformation { get; private set; } = null!;

        public Address Address { get; private set; } = null!;

        public double Weight { get; private set; }

        public double Height { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; } = null!;

        public bool IsNeutered { get; private set; }

        public DateOnly? DateOfBirth { get; private set; }

        public bool IsVaccinated { get; private set; }

        public Constants.StatusPet Status { get; private set; }
        
        public PetDetails PetDetails { get; private set; } = null!;

        public DateTime CreatedDate { get; private set; }

        private readonly List<PetPhoto> _petPhotos = [];

        public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos;
        
        public SpeciesBreed SpeciesBreed { get; private set; }
    }
}
