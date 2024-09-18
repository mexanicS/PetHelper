using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHelper.Domain.Models
{
    public record PetDetails
    {
        private readonly List<DetailsForAssistance> _detailsForAssistances = [];

        public IReadOnlyList<DetailsForAssistance> DetailsForAssistances => _detailsForAssistances;

    }
}
