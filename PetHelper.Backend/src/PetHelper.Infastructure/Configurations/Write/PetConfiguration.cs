using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelper.Domain.Models;
using PetHelper.Domain.Models.Pet;
using PetHelper.Domain.Models.Species;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Infastructure.Configurations.Write
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
            
            builder.ComplexProperty(x => x.Name, tb =>
            {
                tb.Property(d => d.Value)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("name");
            });
            
            builder.ComplexProperty(x => x.TypePet, tb =>
            {
                tb.Property(d => d.Value)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("type_pet");
            });
            
            builder.ComplexProperty(x => x.Description, tb =>
            {
                tb.Property(d => d.Value)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
                    .HasColumnName("description");
            });
            
            builder.ComplexProperty(x => x.Color, tb =>
            {
                tb.Property(d => d.Value)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("color");
            });
            
            builder.ComplexProperty(x => x.HealthInformation, tb =>
            {
                tb.Property(d => d.Value)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("health_information");
            });

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
            
            builder.ComplexProperty(x => x.Weight, tb =>
            {
                tb.Property(d => d.Value)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_MEDIUM_TEXT_LENGTH)
                    .HasColumnName("weight");
            });
            
            builder.ComplexProperty(x => x.Height, tb =>
            {
                tb.Property(d => d.Value)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_MEDIUM_TEXT_LENGTH)
                    .HasColumnName("height");
            });
            
            builder.ComplexProperty(x => x.PhoneNumber, tb =>
            {
                tb.Property(d => d.Value)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_PHONE_LENGTH)
                    .HasColumnName("phone_number");
            });
            
            builder.ComplexProperty(x => x.SerialNumber, tb =>
            {
                tb.Property(d => d.Value)
                    .IsRequired()
                    .HasColumnName("serial_number");
            });

            builder.Property(p => p.IsNeutered)
                .IsRequired();

            builder.Property(p => p.DateOfBirth)
                //.IsRequired()
                ;

            builder.Property(p => p.IsVaccinated)
                .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired();
            
            builder.ComplexProperty(x => x.Position, db =>
            {
                db.Property(z => z.Value)
                    .IsRequired()
                    .HasColumnName("position");
            });
            
            builder.OwnsOne(x => x.PetPhotosList, pp =>
            {
                pp.ToJson("pet_photos_list");
                
                pp.OwnsMany(i => i.PetPhotos, j =>
                {
                    j.Property(k => k.FilePath)
                        .HasConversion(
                            p => p.Value,
                            value => FilePath.Create(value).Value)
                        .HasMaxLength(FilePath.MAX_FILEPATH_LENGTH)
                        .IsRequired()
                        .HasJsonPropertyName("path");

                    j.Property(i => i.IsMain)
                        .IsRequired()
                        .HasColumnName("is_main");

                });
            });

            builder.OwnsOne(x => x.PetDetails, pd =>
            {
                pd.ToJson("pet_details");
                
                pd.OwnsMany(d => d.DetailsForAssistances, rb =>
                {
                    rb.Property(r => r.Name)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                        .HasColumnName("details_for_assistance_name");
                    
                    rb.Property(r => r.Description)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
                        .HasColumnName("details_for_assistance_name");
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
                        .IsRequired()
                        .HasColumnName("breed_id");
                });
            
            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_deleted");
        }
    }
}
