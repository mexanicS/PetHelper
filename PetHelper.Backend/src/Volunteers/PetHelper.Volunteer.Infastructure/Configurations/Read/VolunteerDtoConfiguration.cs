using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelper.Core.DTOs.ReadDtos;

namespace PetHelper.Volunteer.Infastructure.Configurations.Read;

public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDto>
{
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volunteer");

        builder.HasKey(pet => pet.Id);
        
        builder.Property(pet => pet.ExperienceInYears)
            .HasColumnName("experience");
        
        builder.HasMany(pet => pet.Pets)
            .WithOne()
            .HasForeignKey(pet => pet.VolunteerId);
    }
}