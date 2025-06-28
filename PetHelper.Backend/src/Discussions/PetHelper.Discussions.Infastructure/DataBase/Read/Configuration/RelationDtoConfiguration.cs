using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelper.Discussions.Application.Database.Dto;

namespace PetHelper.Discussions.Infastructure.DataBase.Read.Configuration;
public class RelationDtoConfiguration : IEntityTypeConfiguration<RelationDto>
{
    public void Configure(EntityTypeBuilder<RelationDto> builder)
    {
        builder.ToTable("relations");

        builder.HasKey(x => x.Id);
        builder.Property(r => r.Id) 
            .IsRequired()
            .HasColumnName("id");

        builder.Property(r => r.Name) 
            .IsRequired()
            .HasColumnName("name");
    }
}
