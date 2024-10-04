using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHelper.Domain.Shared
{
    public class Constants
    {
        public const int MAX_LOW_TEXT_LENGTH = 100;

        public const int MAX_MEDIUM_TEXT_LENGTH = 1000;

        public const int MAX_HIGH_TEXT_LENGTH = 4000;

        public const int MAX_HIGH_PHONE_LENGTH = 12;

        public const int MAX_PET_SPECIES_TEXT_LENGTH = 100;

        public const int MAX_BREED_TEXT_LENGTH = 100;
        
        public const int MAX_BREED_PHONE_LENGTH = 100;

        public const string BACKET_PHOTO = "photos";
        
        public const int EXPIRY_IN_SECONDS = 60 * 60 * 24;
        
        public enum StatusPet
        {
            NeedsHelp,
            LookingForHome,
            FoundHome
        }
    }
}
