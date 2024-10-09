using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Domain.Models
{
    public record PetDetails
    {
        public IReadOnlyList<DetailsForAssistance> DetailsForAssistances;
        private PetDetails() { }

        public PetDetails(IEnumerable<DetailsForAssistance> detailsForAssistances)
        {
            DetailsForAssistances = detailsForAssistances.ToList();
        }
    }
}
