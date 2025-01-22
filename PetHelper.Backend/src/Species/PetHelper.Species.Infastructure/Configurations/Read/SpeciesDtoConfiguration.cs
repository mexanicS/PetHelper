using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelper.Core.DTOs.ReadDtos;

namespace PetHelper.Species.Infastructure.Configurations.Read;

public class SpeciesDtoConfiguration : IEntityTypeConfiguration<SpeciesDto>
{
    public void Configure(EntityTypeBuilder<SpeciesDto> builder)
    {
        builder.ToTable("species");

        builder.HasKey(pet => pet.Id);
        
        builder.HasMany(pet => pet.Breeds)
            .WithOne()
            .HasForeignKey(pet => pet.SpeciesId);
    }
}