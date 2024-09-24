namespace PetHelper.Domain.ValueObjects;

public record SocialNetworkList
{
    public IReadOnlyList<SocialNetwork> SocialNetwork { get; }

    private SocialNetworkList() { }

    public SocialNetworkList(IEnumerable<SocialNetwork> socialNetwork)
    {
        SocialNetwork = socialNetwork.ToList();
    }
}