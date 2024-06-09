using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class HobbyTypeConfiguration : IEntityTypeConfiguration<Hobby>
{
    public void Configure(EntityTypeBuilder<Hobby> builder)
    {
        builder.Property(h => h.Text).HasMaxLength(50);
        builder.Property(h => h.Icon).HasMaxLength(30);
        builder.HasData(
            new Hobby { Id = 1, Text = "Skating", Icon = "skateboarding" },
            new Hobby { Id = 2, Text = "Coding", Icon = "code" }
        );
    }
}
