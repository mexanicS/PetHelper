using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelper.Core.DTOs.ReadDtos;

namespace PetHelper.Volunteer.Infastructure.Configurations.Read
{
    public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
    {
        public void Configure(EntityTypeBuilder<PetDto> builder)
        {
            builder.ToTable("pet");

            builder.HasKey(pet => pet.Id);
           
        }
    }
}
