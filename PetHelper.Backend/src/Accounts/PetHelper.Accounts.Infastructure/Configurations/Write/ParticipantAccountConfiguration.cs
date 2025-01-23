using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelper.Accounts.Domain.AccountModels;

namespace PetHelper.Accounts.Infastructure.Configurations.Write;

public class ParticipantAccountConfiguration : IEntityTypeConfiguration<ParticipantAccount>
{
    public void Configure(EntityTypeBuilder<ParticipantAccount> builder)
    {
        builder.ToTable("participant_accounts");
        builder.HasKey(x => x.Id);
        
        builder.Property(n => n.BannedForRequestsUntil)
            .IsRequired(false)
            .HasColumnName("banned_for_requests_until");
    }
}