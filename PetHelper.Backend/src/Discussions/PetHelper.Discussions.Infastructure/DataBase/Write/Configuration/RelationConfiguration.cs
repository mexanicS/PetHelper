using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelper.Discussions.Domain;
using PetHelper.SharedKernel.ValueObjects.Discussion;

namespace PetHelper.Discussions.Infastructure.DataBase.Write.Configuration;
public class RelationConfiguration : IEntityTypeConfiguration<Relation>
{
    public void Configure(EntityTypeBuilder<Relation> builder)
    {
        builder.ToTable("relations");

        builder.HasKey(x => x.Id);
        builder.Property(r => r.Id)
            .HasConversion(
                id => id.Value,
                value => RelationId.Create(value).Value)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(r => r.Name)
            .HasConversion(
                id => id.Value,
                value => RelationName.Create(value).Value)
            .IsRequired()
            .HasColumnName("name");
    }
}
