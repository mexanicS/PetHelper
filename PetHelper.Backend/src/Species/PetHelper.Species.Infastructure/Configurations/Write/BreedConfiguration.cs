using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.ModelIds;
using PetHelper.Species.Domain.Models;

namespace PetHelper.Species.Infastructure.Configurations.Write;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breed");

        builder.HasKey(breed => breed.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => BreedId.Create(value));
        
        builder.ComplexProperty(x => x.Name, tb =>
        {
            tb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("name");
        });
    }
}