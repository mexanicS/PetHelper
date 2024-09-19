using PetHelper.Domain.Shared;

namespace PetHelper.Domain.ValueObjects
{
    public record VolunteerDetails
    {
        public IReadOnlyList<SocialNetwork> SocialNetwork { get; private set; } = [];

        public IReadOnlyList<DetailsForAssistance> DetailsForAssistance { get; private set; } = [];

        private VolunteerDetails(
            IReadOnlyList<SocialNetwork> socialNetwork, 
            IReadOnlyList<DetailsForAssistance> detailsForAssistance)
        {
            SocialNetwork = socialNetwork;
            DetailsForAssistance = detailsForAssistance;
        }
        
        public static VolunteerDetails Create(
            IReadOnlyList<SocialNetwork> socialNetwork, 
            IReadOnlyList<DetailsForAssistance> detailsForAssistance)
        {
            return new VolunteerDetails(socialNetwork, detailsForAssistance);
        }
        
    }
}
