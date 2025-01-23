namespace PetHelper.SharedKernel.ValueObjects;

public record DetailsForAssistanceList
{

    public IReadOnlyList<DetailsForAssistance> DetailsForAssistance { get; }

    private DetailsForAssistanceList() { }

    public DetailsForAssistanceList(IEnumerable<DetailsForAssistance> detailsForAssistance)
    {
        DetailsForAssistance = detailsForAssistance.ToList();
    }
    
}