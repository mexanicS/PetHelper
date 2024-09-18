using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;

namespace PetHelper.Infastructure.Configurations
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pet");

            builder.HasKey(pet => pet.Id);
            
            builder.Property(p => p.Id)
                .HasConversion(
                    id => id.Value,
                    value => PetId.Create(value));
            
            builder.Property(pet => pet.Name)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(pet => pet.TypePet)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

            builder.Property(p => p.Breed)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(p => p.Color)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(p => p.HealthInformation)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.ComplexProperty(p => p.Address, a => {
                a.Property(address => address.Street)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("street");

                a.Property(address => address.City)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("city");

                a.Property(address => address.HouseNumber)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("house_number");

                a.Property(address => address.ZipCode)
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("zip_code");
            });

            builder.Property(p => p.Weight);

            builder.Property(p => p.Height);

            builder.Property(p => p.PhoneNumber)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_PHONE_LENGTH);

            builder.Property(p => p.IsNeutered)
                .IsRequired();

            builder.Property(p => p.DateOfBirth)
                .IsRequired();

            builder.Property(p => p.IsVaccinated)
                .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired();

            builder.HasMany(p => p.PetPhotos)
                .WithOne()
                .HasForeignKey("pet_id")
                .IsRequired();

            builder.OwnsOne(x => x.PetDetails, pd =>
            {
                pd.ToJson();
                pd.OwnsMany(d => d.DetailsForAssistances, rb =>
                {
                    rb.Property(r => r.Name)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                    
                    rb.Property(r => r.Description)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
                });
            });
            
            builder.ComplexProperty(p => p.SpeciesBreed,
                pb =>
                {
                    pb.Property(s => s.SpeciesId)
                        .HasConversion(
                            id => id.Value,
                            value => SpeciesId.Create(value))
                        .IsRequired()
                        .HasColumnName("species_id");
                    
                    pb.Property(s => s.BreedId)
                        .HasConversion(
                            id => id.Value,
                            value => BreedId.Create(value))
                        .IsRequired()
                        .HasColumnName("breed_id");
                });
        }


    }
}
