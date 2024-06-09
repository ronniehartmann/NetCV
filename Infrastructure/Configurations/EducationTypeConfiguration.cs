using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class EducationTypeConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.Property(e => e.School).HasMaxLength(100);
        builder.Property(e => e.Title).HasMaxLength(100);
        builder.HasData(
            new Education { Id = 1, School = "BMS Zürich", Title = "Technische Maturität", StartDate = new DateOnly(2020, 8, 1) }
        );
    }
}
