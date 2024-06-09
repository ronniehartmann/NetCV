using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SkillTypeConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.Property(s => s.Name).HasMaxLength(50);
        builder.ToTable(t => t.HasCheckConstraint("CK_Skill_Level", "'Level' >= 0 AND 'Level' <= 100"));
        builder.HasData(
            new Skill { Id = 1, Name = "C#", Level = 80 },
            new Skill { Id = 2, Name = "Java", Level = 50 },
            new Skill { Id = 3, Name = ".NET Framework", Level = 70 }
        );
    }
}
