﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHelper.Domain.Models
{
    public class Volunteer
    {
        public Guid Id { get; set; }

        public FullName Name { get; private set; } = null!;

        public string Email { get; private set; } = null!;

        public string Description { get; private set; } = null!;

        public int ExperienceInYears { get; private set; }

        public string PhoneNumber { get; private set; } = null!;

        public IReadOnlyList<SocialNetwork>? SocialNetwork { get; private set; }

        public IReadOnlyList<DetailsForAssistance> DetailsForAssistance { get; private set; } = [];

        public List<Pet> Pets = [];

        /// <summary>
        /// Количество домашних животных, которые нашли новый дом
        /// </summary>
        /// <returns>Число</returns>
        public int GetCountOfAnimalsFoundHome()
        {
            return Pets.Where(pet=>pet.Status == StatusPet.FoundHome).Count(); 
        }

        /// <summary>
        /// Количество домашних животных, которые ищут дом
        /// </summary>
        /// <returns>Число</returns>
        public int GetCountOfAnimalsNeedsHelp()
        {
            return Pets.Where(pet => pet.Status == StatusPet.LookingForHome).Count();
        }

        /// <summary>
        /// Количество домашних животных, которым требуется лечение
        /// </summary>
        /// <returns>Число</returns>
        public int GetCountOfAnimalsLookingForHome()
        {
            return Pets.Where(pet => pet.Status == StatusPet.NeedsHelp).Count();
        }
    }
}