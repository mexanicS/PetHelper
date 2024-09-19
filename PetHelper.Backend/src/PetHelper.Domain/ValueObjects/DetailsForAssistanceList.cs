namespace PetHelper.Domain.ValueObjects;

public record DetailsForAssistanceList
{
    public IReadOnlyList<DetailsForAssistance> DetailsForAssistances { get; }
    
    private DetailsForAssistanceList()
    {
    }
    
    private DetailsForAssistanceList(IEnumerable<DetailsForAssistance> detailsForAssistance)
    {
        DetailsForAssistances = detailsForAssistance.ToList();
    }

    public static DetailsForAssistanceList Create(IEnumerable<DetailsForAssistance> detailsForAssistance) => new DetailsForAssistanceList();
}