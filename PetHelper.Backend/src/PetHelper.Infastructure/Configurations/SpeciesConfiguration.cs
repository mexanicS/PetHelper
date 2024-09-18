using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;

namespace PetHelper.Infastructure.Configurations;

public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");

        builder.HasKey(species => species.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value));

        builder.Property(species => species.Name)
            .HasMaxLength(Constants.MAX_PET_SPECIES_TEXT_LENGTH);

        builder.HasMany(species => species.Breeds)
            .WithOne()
            .HasForeignKey("species_id");
    }
}