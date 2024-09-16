using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetHelper.Domain.Shared;

namespace PetHelper.Domain.Models
{
    public class Volunteer : Entity<VolunteerId>
    {
        private Volunteer(VolunteerId id) : base(id)
        {
        }

        private Volunteer(VolunteerId volunteerId, 
            
            string email,
            string description,
            int experienceInYears,
            string phoneNumber) 
            : base(volunteerId)
        {
            Id = volunteerId;
            Email = email;
            Description = description;
            ExperienceInYears = experienceInYears;
            PhoneNumber = phoneNumber;
        }
        public VolunteerId Id { get; private set; }

        public FullName Name { get; private set; } = null!;

        public string Email { get; private set; } = null!;

        public string Description { get; private set; } = null!;

        public int ExperienceInYears { get; private set; }

        public string PhoneNumber { get; private set; } = null!;

        public VolunteerDetails VolunteerDetails { get; private set; } = null!;

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
    }
}
