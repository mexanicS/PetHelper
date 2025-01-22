using CSharpFunctionalExtensions;

namespace PetHelper.SharedKernel.ValueObjects
{
    public record DetailsForAssistance
    {
        public const int MAX_LENGTH = 100;
        public string Name { get; }
        public string Description { get; }
        private DetailsForAssistance(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public static Result<DetailsForAssistance, Error> Create(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name) || MAX_LENGTH < name.Length)
                return Errors.General.ValueIsInvalid("name");

            if (string.IsNullOrWhiteSpace(description) || MAX_LENGTH < name.Length)
                return Errors.General.ValueIsInvalid("description");

            return new DetailsForAssistance(name, description);
        }
    }
}
