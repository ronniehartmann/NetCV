using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ReferenceTypeConfiguration : IEntityTypeConfiguration<Reference>
{
    public void Configure(EntityTypeBuilder<Reference> builder)
    {
        builder.Property(r => r.Title).HasMaxLength(30);
        builder.Property(r => r.Employment).HasMaxLength(30);
        builder.Property(r => r.Text).HasMaxLength(500);
        builder.HasData(
            new Reference { Id = 1, Title = "Max Mustermann", Employment = "Applikationsentwickler", Text = "Hey!" }
        );
    }
}
