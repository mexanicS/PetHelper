using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelper.Accounts.Domain;
using PetHelper.Core.DTOs;
using PetHelper.Core.DTOs.Pet;
using PetHelper.Core.Extensions;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects;
using PetHelper.SharedKernel.ValueObjects.Pet;

namespace PetHelper.Accounts.Infastructure.Configurations.Write;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.ComplexProperty(v => v.FullName, fn =>
        {
            fn.Property(n => n.FirstName)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("name");
            
            fn.Property(n => n.LastName)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("last_name");
            
            fn.Property(n => n.MiddleName)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("middle_name");
        });
        
        builder
            .Property(u => u.SocialNetworks)
            .ValueObjectsJsonConversion(
                input => new SocialNetworkDto( input.Name , input.Url),
                output => SocialNetwork.Create(output.Name, output.Url).Value)
            .HasColumnName("social_networks");
        
        builder.Property(v => v.Photos)
            .ValueObjectsJsonConversion<PetPhoto, PetPhotoDto>(
                file => new PetPhotoDto {Path = file.FilePath , IsMain = file.IsMain},
                json => new PetPhoto {IsMain = json.IsMain, FilePath = json.Path})
            .HasColumnName("photos");

        builder.HasMany(u => u.Roles)
            .WithMany()
            .UsingEntity<IdentityUserRole<Guid>>();
        
    }
}