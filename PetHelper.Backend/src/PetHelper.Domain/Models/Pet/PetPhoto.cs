using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHelper.Domain.Models
{
    public class PetPhoto
    {
        public PetPhoto()
        {
            
        }
        public Guid Id { get; set; }

        public string FilePath { get; set; } = null!;

        public bool IsMain { get; set; }
    }
}
