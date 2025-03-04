using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelper.SharedKernel;
using PetHelper.Volunteer.Domain.Ids;

namespace PetHelper.Volunteer.Infastructure.Configurations.Write
{
    public class VolunteerConfiguration : IEntityTypeConfiguration<Domain.Volunteer>
    {
        public void Configure(EntityTypeBuilder<Domain.Volunteer> builder)
        {
            builder.ToTable("volunteer");

            builder.HasKey(volunteer => volunteer.Id);
            
            builder.Property(p => p.Id)
                .HasConversion(
                    id => id,
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
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            
            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_deleted");
            
            //builder.HasQueryFilter()
        }
    }
}
