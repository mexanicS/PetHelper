using CSharpFunctionalExtensions;
using PetHelper.Domain.Shared;

namespace PetHelper.Domain.ValueObjects
{
    public record SocialNetwork
    {
        public const int SOCIAL_NETWORK_LENGTH = 200;
        private SocialNetwork(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string Name { get; }
        public string Url { get; }

        public static Result<SocialNetwork, Error> Create(string name, string url)
        {
            if (string.IsNullOrWhiteSpace(name) && name.Length < SOCIAL_NETWORK_LENGTH)
                return Errors.General.ValueIsInvalid("name");
            
            if (string.IsNullOrWhiteSpace(url) && url.Length < SOCIAL_NETWORK_LENGTH)
                return Errors.General.ValueIsInvalid("url");
            
            return new SocialNetwork(name, url);
        }
    }
}
