namespace PetHelper.Domain.Models
{
    public record SocialNetwork
    {
        public string Name { get; set; } = null!;

        public string Url { get; set; } = null!;
    }
}
