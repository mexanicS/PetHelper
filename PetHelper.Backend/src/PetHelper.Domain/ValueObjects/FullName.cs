using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHelper.Domain.Models
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
    }
}
