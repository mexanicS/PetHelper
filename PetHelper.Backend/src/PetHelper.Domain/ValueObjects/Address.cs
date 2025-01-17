using CSharpFunctionalExtensions;
using PetHelper.Domain.Shared;

namespace PetHelper.Domain.ValueObjects
{
    public record Address
    {
        public const int MAX_LENGTH = 200;
        public string City { get; private set; } = null!;

        public string Street { get; private set; } = null!;

        public string HouseNumber { get; private set; } = null!;

        public string? ZipCode { get; private set; }

        public override string ToString()
        {
            return $"{Street}, {HouseNumber}, {City}{(ZipCode != null ? ", " + ZipCode : "")}";
        }

        public Address(string city, string street, string houseNumber, string? zipCode)
        {
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            ZipCode = zipCode;
        }
        
        public static Result<Address, Error> Create(string city, string street, string houseNumber, string? zipCode)
        {
            if (string.IsNullOrWhiteSpace(city) || city.Length > MAX_LENGTH)
                return Errors.General.ValueIsInvalid("city");
            
            if (string.IsNullOrWhiteSpace(street) || street.Length > MAX_LENGTH)
                return Errors.General.ValueIsInvalid("street");
            
            if (string.IsNullOrWhiteSpace(houseNumber) || houseNumber.Length > MAX_LENGTH)
                return Errors.General.ValueIsInvalid("houseNumber");
            
            if (street.Length > MAX_LENGTH)
                return Errors.General.ValueIsInvalid("zipCode");
            
            return new Address(city, street, houseNumber, zipCode);
        }
    }
}
