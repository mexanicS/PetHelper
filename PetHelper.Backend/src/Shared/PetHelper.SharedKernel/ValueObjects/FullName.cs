using CSharpFunctionalExtensions;

namespace PetHelper.SharedKernel.ValueObjects
{
    public record FullName
    {
        public const int MAX_LENGTH = 100;
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string? MiddleName { get; private set; }

        public FullName(string firstName, 
                        string lastName, 
                        string? middleName = null)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName}" + (MiddleName != null ? $" {MiddleName}" : "");
        }
        
        public static Result<FullName, Error> Create(string firstName, string lastName, string? middleName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > MAX_LENGTH)
                return Errors.General.ValueIsInvalid("firstName");
            
            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > MAX_LENGTH)
                return Errors.General.ValueIsInvalid("lastName");
            
            if (middleName?.Length > 100)
                return Errors.General.ValueIsInvalid("middleName");
            
            return new FullName(firstName, lastName, middleName);
        }
    }
}
