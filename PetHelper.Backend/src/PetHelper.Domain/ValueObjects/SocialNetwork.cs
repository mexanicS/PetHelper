namespace PetHelper.Domain.ValueObjects
{
    public record SocialNetwork
    {
        private SocialNetwork(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string Name { get; }
        public string Url { get; }

        public static SocialNetwork Create(string name, string url) => new SocialNetwork(name, url);
    }
}
