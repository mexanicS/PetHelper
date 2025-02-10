using CSharpFunctionalExtensions;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects;
using PetHelper.SharedKernel.ValueObjects.Common;
using PetHelper.SharedKernel.ValueObjects.Pet;
using PetHelper.Volunteer.Domain.Ids;

namespace PetHelper.Volunteer.Domain
{
    public class Volunteer : SharedKernel.Entity<VolunteerId>, ISoftDeletable
    {
        private Volunteer(VolunteerId id) : base(id)
        {
        }

        public Volunteer(VolunteerId volunteerId,
            FullName fullName,
            Email email,
            Description description,
            ExperienceInYears experienceInYears,
            PhoneNumber phoneNumber) 
            : base(volunteerId)
        {
            Name = fullName;
            Email = email;
            Description = description;
            ExperienceInYears = experienceInYears;
            PhoneNumber = phoneNumber;
        }

        public FullName Name { get; private set; } = null!;

        public Email Email { get; private set; } = null!;
        
        public Description Description { get; private set; } = null!;

        public ExperienceInYears ExperienceInYears { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; } = null!;

        private bool _isDeleted;

        private readonly List<Pet> _pets = [];

        public IReadOnlyList<Pet> Pets => _pets;

        /// <summary>
        /// Количество домашних животных, которые нашли новый дом
        /// </summary>
        /// <returns>Число</returns>
        public int GetCountOfAnimalsFoundHome()
        {
            return Pets.Count(pet => pet.Status == Constants.StatusPet.FoundHome); 
        }

        /// <summary>
        /// Количество домашних животных, которые ищут дом
        /// </summary>
        /// <returns>Число</returns>
        public int GetCountOfAnimalsNeedsHelp()
        {
            return Pets.Count(pet => pet.Status == Constants.StatusPet.LookingForHome);
        }

        /// <summary>
        /// Количество домашних животных, которым требуется лечение
        /// </summary>
        /// <returns>Число</returns>
        public int GetCountOfAnimalsLookingForHome()
        {
            return Pets.Count(pet => pet.Status == Constants.StatusPet.NeedsHelp);
        }

        public void UpdateMainInformation(FullName fullName,
            Email email, 
            Description description, 
            ExperienceInYears experienceInYears,
            PhoneNumber phoneNumber)
        {
            Name = fullName;
            Email = email;
            Description = description;
            ExperienceInYears = experienceInYears;
            PhoneNumber = phoneNumber;
        }

        public void Delete()
        {
           _isDeleted = true;
           foreach (var pet in _pets)
               pet.Delete();
           
        }

        public void Restore()
        {
            _isDeleted = false;
            foreach (var pet in _pets)
                pet.Restore();
        }

        public UnitResult<Error> AddPet(Pet pet)
        {
            var position = Position.Create(_pets.Count + 1);

            if (position.IsFailure)
                return position.Error;

            pet.UpdatePosition(position.Value);
            
            pet.SetSerialNumber(SerialNumber.Create(1).Value);
            
            _pets.Add(pet);
            
            return Result.Success<Error>();
        }

        public Result<Pet, Error> GetPetById(PetId petId)
        {
            var pet = _pets.FirstOrDefault(pet => pet.Id == petId);

            if (pet is null)
                return Errors.General.NotFound(petId.Value);

            return pet;
        }
        
        public UnitResult<Error> MovePet(Pet pet, Position newPosition)
        {
            var currentPosition = pet.Position;
            
            if (currentPosition == newPosition || _pets.Count == 1)
                return Result.Success<Error>();
            
            var ajustedPosition = AjustPositionIfOutOfRange(newPosition);
            
            if (ajustedPosition.IsFailure)
                return ajustedPosition.Error;
            
            newPosition = ajustedPosition.Value;
            
            var arrangeResult = ArrangePets(newPosition, currentPosition);
            
            if (arrangeResult.IsFailure)
                return arrangeResult.Error;
            
            pet.MoveToPosition(newPosition);
            
            return Result.Success<Error>();
        }
        
        private Result<Position, Error> AjustPositionIfOutOfRange(Position newPosition)
        {
            if (newPosition.Value <= _pets.Count)
                return newPosition;
            
            var lastPosition = Position.Create(_pets.Count);
            
            if (lastPosition.IsFailure)
                return lastPosition.Error;
            
            return lastPosition.Value;
        }
        
        private UnitResult<Error> ArrangePets(Position newPosition, Position currentPosition)
        {
            if (newPosition < currentPosition)
            {
                var petsToMove = _pets
                    .Where(x => x.Position >= newPosition && x.Position < currentPosition);
                foreach (var petToMove in petsToMove)
                {
                    var result = petToMove.MoveForward();
                    
                    if (result.IsFailure)
                        return result.Error;
                }
            }
            else
            {
                var petsToMove = _pets
                    .Where(x => x.Position > currentPosition && x.Position <= newPosition);
                foreach (var petToMove in petsToMove)
                {
                    var result = petToMove.MoveBackward();
                    
                    if (result.IsFailure)
                        return result.Error;
                }
            }
            return Result.Success<Error>();
        }

        public void RemovePet(Pet pet)
        {
            _pets.Remove(pet);
        }

        public UnitResult<ErrorList> SetMainPetPhoto(PetId petId, PetPhoto photo)
        {
            var pet = _pets.FirstOrDefault(pet => pet.Id == petId);

            if (pet is null)
                return Errors.General.NotFound(petId.Value).ToErrorList();

            var result = pet.SetMainPhoto(photo.FilePath);
            
            return result;
        }
    }
}
