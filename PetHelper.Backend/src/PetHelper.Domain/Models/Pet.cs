

using CSharpFunctionalExtensions;

namespace PetHelper.Domain.Models
{
    public class Pet
    {
        //ef core
        private Pet() { }

        private Pet(string name, string typePet, string description)
        {
            Name = name;
            TypePet = typePet;
            Description = description;
        }

        public Guid Id { get; private set; }

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

        public StatusPet Status { get; private set; }
        
        public PetDetails PetDetails { get; private set; } = null!;

        public DateTime CreatedDate { get; private set; }

        private readonly List<PetPhoto> _petPhotos = [];

        public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos;

        public static Result<Pet> Create(string name, string typePet, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Pet>("name can not be empty");

            if (string.IsNullOrWhiteSpace(typePet))
                return Result.Failure<Pet>("typePet can not be empty");

            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<Pet>("description can not be empty");

            var pet = new Pet(name, typePet, description);
             
            return Result.Success(pet);
        }

    }
}
