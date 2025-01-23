namespace PetHelper.SharedKernel.ValueObjects
{
    public record PetDetails
    {
        public IReadOnlyList<DetailsForAssistance> DetailsForAssistances;
        private PetDetails() { }

        public PetDetails(IEnumerable<DetailsForAssistance> detailsForAssistances)
        {
            DetailsForAssistances = detailsForAssistances.ToList();
        }
    }
}
