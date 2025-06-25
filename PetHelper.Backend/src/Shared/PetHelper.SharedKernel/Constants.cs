namespace PetHelper.SharedKernel
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
        
        public const string DATABASE = "PetHelperConnectionString";
        
        public const string VOLUNTEER_UNIT_OF_WORK_KEY = "VolunteerUnitOfWorkKey";
        public const string SPECIES_UNIT_OF_WORK_KEY = "SpeciesUnitOfWorkKey";
        public const string ACCOUNT_UNIT_OF_WORK_KEY = "AccountUnitOfWorkKey";
        public const string DISCUSSION_UNIT_OF_WORK_KEY = "DiscussionUnitOfWorkKey";
        public const string VOLUNTEER_REQUEST_UNIT_OF_WORK_KEY = "VolunteerRequestUnitOfWorkKey";
        
        public enum StatusPet
        {
            NeedsHelp,
            LookingForHome,
            FoundHome
        }

        public enum Context
        {
            VolunteerManagement,
            SpeciesManagement,
            AccountManagement,
            Discussions,
            VolunteersRequest
        }

        public enum VolunteerRequestStatus
        {
            Submitted,
            Rejected,
            RevisionRequired,
            Approved,
            OnReview
        }

        public enum DiscussionStatus
        {
            Open,
            Close,
        }
    }
}
