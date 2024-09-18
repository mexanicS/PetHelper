using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;

namespace PetHelper.Infastructure.Configurations;

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
        
        builder.Property(breed => breed.Name)
            .HasMaxLength(Constants.MAX_BREED_TEXT_LENGTH);
    }
}