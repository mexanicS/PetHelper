using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHelper.Domain.Models
{
    public record VolunteerDetails
    {
        public IReadOnlyList<SocialNetwork> SocialNetwork { get; private set; } = [];

        public IReadOnlyList<DetailsForAssistance> DetailsForAssistance { get; private set; } = [];
    }
}
