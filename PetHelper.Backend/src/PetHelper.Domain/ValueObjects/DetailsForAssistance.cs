using PetHelper.Domain.Shared;

namespace PetHelper.Domain.ValueObjects
{
    public record DetailsForAssistance
    {
        public string Name { get; }
        public string Description { get; }
        private DetailsForAssistance(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public static Result<DetailsForAssistance> Create(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                return $"DetailsForAssistance {nameof(name)} can't be empty";

            if (string.IsNullOrWhiteSpace(description))
                return $"DetailsForAssistance {nameof(description)} can't be empty";

            return new DetailsForAssistance(name, description);
        }
    }
}
