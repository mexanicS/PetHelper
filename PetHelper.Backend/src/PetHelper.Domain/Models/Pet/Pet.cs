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
            string name, 
            string typePet, 
            string description,
            string breed,
            string color,
            string healthInformation,
            double weight,
            double height,
            string phoneNumber,
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

        public string Name { get; private set; } = null!;

        public string TypePet { get; private set; } = null!;

        public string Description { get; private set; } = null!;

        public string Breed { get; private set; } = null!;

        public string Color { get; private set; } = null!;

        public string HealthInformation { get; private set; } = null!;

        public Address Address { get; private set; } = null!;

        public double Weight { get; private set; }

        public double Height { get; private set; }

        public string PhoneNumber { get; private set; } = null!;

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
