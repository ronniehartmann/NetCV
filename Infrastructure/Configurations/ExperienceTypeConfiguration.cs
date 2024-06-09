using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ExperienceTypeConfiguration : IEntityTypeConfiguration<Experience>
{
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.Property(e => e.Company).HasMaxLength(50);
        builder.Property(e => e.Company).HasMaxLength(250);
        builder.Property(e => e.Text).HasMaxLength(500);
        builder.HasData(
            new Experience { Id = 1, Company = "Contoso", Text = "Hello!", StartDate = new DateOnly(2020, 8, 1) },
            new Experience { Id = 2, Company = "Musterfirma", Text = "Hey", StartDate = new DateOnly(2016, 1, 1), EndDate = new DateOnly(2020, 7, 31) }
        );
    }
}
