using CSharpFunctionalExtensions;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;
using PetHelper.Domain.ValueObjects.Common;
using PetHelper.Domain.ValueObjects.Pet;

namespace PetHelper.Domain.Models.Pet
{
    public class Pet : Shared.Entity<PetId>
    {
        //ef core
        private Pet(PetId id) : base(id)
        { }
        public Pet(PetId petId,
            Name name, 
            TypePet typePet, 
            Description description,
            Color color,
            HealthInformation healthInformation,
            Weight weight,
            Height height,
            PhoneNumber phoneNumber,
            bool isNeutered,
            DateOnly? birthDate,
            bool isVaccinated,
            DateTime createdDate,
            Address address,
            SpeciesBreed speciesBreed,
            PetDetails petDetails,
            PetPhotoList photos
                )
            : base(petId)
        {
            Name = name;
            TypePet = typePet;
            Description = description;
            Color = color;
            HealthInformation = healthInformation;
            Weight = weight;
            Height = height;
            PhoneNumber = phoneNumber;
            IsNeutered = isNeutered;
            DateOfBirth = birthDate;
            IsVaccinated = isVaccinated;
            CreatedDate = createdDate;
            Address = address;
            SpeciesBreed = speciesBreed;
            PetDetails = petDetails;
            PetPhotosList = photos;
        }

        public Name Name { get; private set; } = null!;

        public TypePet TypePet { get; private set; } = null!;

        public Description Description { get; private set; } = null!;

        public Color Color { get; private set; } = null!;

        public HealthInformation HealthInformation { get; private set; } = null!;

        public Address Address { get; private set; } = null!;

        public Weight Weight { get; private set; }

        public Height Height { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; } = null!;

        public bool IsNeutered { get; private set; }

        public DateOnly? DateOfBirth { get; private set; }

        public bool IsVaccinated { get; private set; }

        public Constants.StatusPet Status { get; private set; }
        
        public PetDetails PetDetails { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public PetPhotoList PetPhotosList { get; private set; }
        
        public SpeciesBreed SpeciesBreed { get; private set; }

        public SerialNumber SerialNumber { get; private set; }
        
        public Position Position { get; private set; } = null!;
        
        private bool _isDeleted = false;

        public void UpdatePhotos(PetPhotoList petPhotoList)
        {
            PetPhotosList = petPhotoList;
        }
        
        public void Delete()
        {
            _isDeleted = true;
        }

        public void Restore()
        {
            _isDeleted = false;
        }

        public void SetSerialNumber(SerialNumber serialNumber)
        {
            SerialNumber = serialNumber;
        }
        
        public void UpdatePosition(Position number)
            => Position = number;
        
        public UnitResult<Error> MoveForward()
        {
            var newPosition = Position.Forward();
            
            if (newPosition.IsFailure)
                return newPosition.Error;
            
            Position = newPosition.Value;
            
            return Result.Success<Error>();
        }
        public UnitResult<Error> MoveBackward()
        {
            var newPosition = Position.Backward();
            
            if (newPosition.IsFailure)
                return newPosition.Error;
            
            Position = newPosition.Value;
            
            return Result.Success<Error>();
        }
        
        public void MoveToPosition(Position position)
        {
            Position = position;
        }

        public void Update(Pet pet)
        {
            Name = pet.Name;
            TypePet = pet.TypePet;
            Description = pet.Description;
            Color = pet.Color;
            HealthInformation = pet.HealthInformation;
            Weight = pet.Weight;
            Height = pet.Height;
            PhoneNumber = pet.PhoneNumber;
            IsNeutered = pet.IsNeutered;
            DateOfBirth = pet.DateOfBirth;
            IsVaccinated = pet.IsVaccinated;
            CreatedDate = pet.CreatedDate;
            Address = pet.Address;
            SpeciesBreed = pet.SpeciesBreed;
            PetDetails = pet.PetDetails;
            PetPhotosList = pet.PetPhotosList;
        }

        public void ChangeStatus(Constants.StatusPet status)
        {
            Status = status;
        }

        public UnitResult<ErrorList> SetMainPhoto(string pathPhoto)
        {
            var oldPhoto = PetPhotosList.PetPhotos.FirstOrDefault(p => p.FilePath.Value == pathPhoto);
            if(oldPhoto is null)
                return Errors.Pet.PhotoNotFound().ToErrorList();
            
            var oldMainPhoto =  PetPhotosList.PetPhotos.FirstOrDefault(p => p.IsMain);
            
            if (oldMainPhoto != null)
            {
                var oldMainPhotoWithNewProperty = PetPhoto.Create(oldMainPhoto.FilePath).Value;
                PetPhotosList.PetPhotos.Remove(oldMainPhoto);
                PetPhotosList.PetPhotos.Add(oldMainPhotoWithNewProperty);
            }
            PetPhotosList.PetPhotos.Remove(oldPhoto);
            
            var mainPhoto = PetPhoto.Create(FilePath.Create(pathPhoto).Value, true).Value;
            PetPhotosList.PetPhotos.Add(mainPhoto);
            
            UpdatePhotos(new PetPhotoList(PetPhotosList.PetPhotos.OrderByDescending(p=>p.IsMain)));

            return Result.Success<ErrorList>();
        }
    }
}
