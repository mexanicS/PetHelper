using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHelper.Domain.Models
{
    public class Address
    {
        public string City { get; private set; } = null!;

        public string Street { get; private set; } = null!;

        public string HouseNumber { get; private set; } = null!;

        public string? ZipCode { get; private set; }

        public override string ToString()
        {
            return $"{Street}, {HouseNumber}, {City}{(ZipCode != null ? ", " + ZipCode : "")}";
        }
    }
}
