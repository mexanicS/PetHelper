using PetHelper.Domain.Shared;

namespace PetHelper.Domain.ValueObjects
{
    public record VolunteerDetails
    {
        private VolunteerDetails() { }
        public IReadOnlyList<SocialNetwork> SocialNetwork { get; private set; } = [];

        public IReadOnlyList<DetailsForAssistance> DetailsForAssistance { get; private set; } = [];

        private VolunteerDetails(
            IEnumerable<SocialNetwork> socialNetwork, 
            IEnumerable<DetailsForAssistance> detailsForAssistance)
        {
            SocialNetwork = socialNetwork.ToList();
            DetailsForAssistance = detailsForAssistance.ToList();
        }
        
        public static VolunteerDetails Create(
            IEnumerable<SocialNetwork> socialNetwork, 
            IEnumerable<DetailsForAssistance> detailsForAssistance)
        {
            return new VolunteerDetails(socialNetwork, detailsForAssistance);
        }
        
    }
}
