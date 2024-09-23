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
            builder.ToTable("volunteer");

            builder.HasKey(volunteer => volunteer.Id);
            
            builder.Property(p => p.Id)
                .HasConversion(
                    id => id.Value,
                    value => VolunteerId.Create(value));
            
            builder.ComplexProperty(v => v.Name, nameBuilder =>
            {
                nameBuilder.Property(n => n.FirstName)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("first_name");

                nameBuilder.Property(n => n.LastName)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("last_name");

                nameBuilder.Property(n => n.MiddleName)
                    .IsRequired(false)
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("middle_name");
            });
            
            builder.ComplexProperty(x => x.Email, tb =>
            {
                tb.Property(d => d.Value)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
                    .HasColumnName("email");
            });
            
            builder.ComplexProperty(x => x.Description, tb =>
            {
                tb.Property(d => d.Value)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
                    .HasColumnName("description");
            });
            
            builder.ComplexProperty(x => x.ExperienceInYears, tb =>
            {
                tb.Property(d => d.Value)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
                    .HasColumnName("experience");
            });
            
            builder.ComplexProperty(x => x.PhoneNumber, tb =>
            {
                tb.Property(d => d.Value)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
                    .HasColumnName("phone_number");
            });

            builder.HasMany(volunteer => volunteer.Pets)
                .WithOne()
                .HasForeignKey("volunteer_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(v => v.DetailsForAssistance, vb =>
            {
                vb.ToJson("details_for_assistance");

                vb.OwnsMany(cl => cl.DetailsForAssistance, cl =>
                {
                    cl.Property(c => c.Name)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                        .HasColumnName("details_for_assistance_name");

                    cl.Property(c => c.Description)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_MEDIUM_TEXT_LENGTH)
                        .HasColumnName("details_for_assistance_description"); ;
                });
            });

            builder.OwnsOne(v => v.SocialNetwork, vb =>
            {
                vb.ToJson("social_network");

                vb.OwnsMany(cl => cl.SocialNetwork, cl =>
                {
                    cl.Property(c => c.Name)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                        .HasColumnName("social_network_name");


                    cl.Property(c => c.Url)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_MEDIUM_TEXT_LENGTH)
                        .HasColumnName("social_network_link");
                });
            });
        }
    }
}
