﻿namespace PetHelper.Domain.Models
{
    public record FullName
    {
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
        
        public static FullName Create(string firstName, string lastName, string? middleName) =>
            new FullName(firstName, lastName, middleName);
    }
}
