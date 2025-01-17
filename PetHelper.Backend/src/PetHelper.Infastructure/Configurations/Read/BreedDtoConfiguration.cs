using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelper.Application.DTOs.Pet;
using PetHelper.Application.DTOs.ReadDtos;

namespace PetHelper.Infastructure.Configurations.Read;

public class BreedDtoConfiguration : IEntityTypeConfiguration<BreedDto>
{
    public void Configure(EntityTypeBuilder<BreedDto> builder)
    {
        builder.ToTable("breed");

        builder.HasKey(pet => pet.Id);
    }
}