using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelper.Application.DTOs;
using PetHelper.Application.DTOs.ReadDtos;

namespace PetHelper.Infastructure.Configurations.Read;

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