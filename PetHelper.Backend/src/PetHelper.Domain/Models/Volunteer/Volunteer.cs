using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Domain.Models
{
    public class Volunteer : Entity<VolunteerId>
    {
        private Volunteer(VolunteerId id) : base(id)
        {
        }

        public Volunteer(VolunteerId volunteerId,
            FullName fullName,
            Email email,
            Description description,
            ExperienceInYears experienceInYears,
            PhoneNumber phoneNumber,
            SocialNetworkList socialNetworks,
            DetailsForAssistanceList detailsForAssistances) 
            : base(volunteerId)
        {
            Id = volunteerId;
            Name = fullName;
            Email = email;
            Description = description;
            ExperienceInYears = experienceInYears;
            PhoneNumber = phoneNumber;
            SocialNetwork = socialNetworks;
            DetailsForAssistance = detailsForAssistances;
        }
        public VolunteerId Id { get; private set; }

        public FullName Name { get; private set; } = null!;

        public Email Email { get; private set; } = null!;
        
        public Description Description { get; private set; } = null!;

        public ExperienceInYears ExperienceInYears { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; } = null!;
        
        public SocialNetworkList SocialNetwork { get; private set; }

        public DetailsForAssistanceList DetailsForAssistance { get; private set; } 
        

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
