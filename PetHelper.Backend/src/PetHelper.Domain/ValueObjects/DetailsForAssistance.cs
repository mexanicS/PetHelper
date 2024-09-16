﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHelper.Domain.Models
{
    public record DetailsForAssistance
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}