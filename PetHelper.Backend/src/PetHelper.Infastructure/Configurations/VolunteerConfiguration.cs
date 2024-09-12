using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;

namespace PetHelper.Infastructure.Configurations
{
    public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.ToTable("Volunteer");

            builder.HasKey(volunteer => volunteer.Id);

            builder.OwnsOne(v => v.Name, nameBuilder =>
            {
                nameBuilder.Property(n => n.FirstName)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                nameBuilder.Property(n => n.LastName)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                nameBuilder.Property(n => n.MiddleName)
                    .IsRequired(false)
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
            });

            builder.Property(volunteer => volunteer.Email)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(volunteer => volunteer.Description)
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
                .IsRequired();

            builder.Property(volunteer => volunteer.ExperienceInYears)
                .IsRequired();

            builder.Property(volunteer => volunteer.PhoneNumber)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_PHONE_LENGTH);

            builder.HasMany(volunteer => volunteer.Pets)
                .WithOne()
                .HasForeignKey("VolunteerId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(x => x.VolunteerDetails, vd =>
            {
                vd.ToJson();
                vd.OwnsMany(d => d.DetailsForAssistance, r =>
                {
                    r.Property(x => x.Name)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                    r.Property(x => x.Description)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
                });
                vd.OwnsMany(d => d.SocialNetwork, r =>
                {

                    r.Property(x => x.Name)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                    r.Property(x => x.Url)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_MEDIUM_TEXT_LENGTH);
                });
            });
        }


    }
}
