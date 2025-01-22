using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.ModelIds;

namespace PetHelper.Species.Infastructure.Configurations.Write;

public class SpeciesConfiguration : IEntityTypeConfiguration<Domain.Models.Species>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Species> builder)
    {
        builder.ToTable("species");

        builder.HasKey(species => species.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value));
        
        builder.ComplexProperty(x => x.Name, tb =>
        {
            tb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_PET_SPECIES_TEXT_LENGTH)
                .HasColumnName("name");
        });

        builder.HasMany(species => species.Breeds)
            .WithOne()
            .HasForeignKey("species_id");
    }
}