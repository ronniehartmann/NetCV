using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public partial class CvContext : DbContext
{
    public CvContext(DbContextOptions<CvContext> options)
            : base(options)
    {
    }

    public DbSet<Content> Contents { get; set; }

    public DbSet<Experience> Experiences { get; set; }

    public DbSet<Hobby> Hobbies { get; set; }

    public DbSet<Skill> Skills { get; set; }

    private const string DEFAULT_VALUE = "Edit the value in the administrator panel.";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Content>().HasKey(c => c.Key);

        modelBuilder.Entity<Experience>().Property(e => e.Company).HasMaxLength(50);
        modelBuilder.Entity<Experience>().Property(e => e.Text).HasMaxLength(500);

        modelBuilder.Entity<Hobby>().Property(h => h.Text).HasMaxLength(50);
        modelBuilder.Entity<Hobby>().Property(h => h.Icon).HasMaxLength(30);

        modelBuilder.Entity<Reference>().Property(r => r.Title).HasMaxLength(30);
        modelBuilder.Entity<Reference>().Property(r => r.Employment).HasMaxLength(30);
        modelBuilder.Entity<Reference>().Property(r => r.Text).HasMaxLength(500);

        modelBuilder.Entity<Skill>().Property(s => s.Name).HasMaxLength(50);
        modelBuilder.Entity<Skill>()
            .ToTable(t => t.HasCheckConstraint("CK_Skill_Level", "'Level' >= 0 AND 'Level' <= 100"));

        Seed(modelBuilder);
    }

    private void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Content>().HasData(
            new Content { Key = "PROFILE_FULLNAME", Value = DEFAULT_VALUE },
            new Content { Key = "PROFILE_EMPLOYMENT", Value = DEFAULT_VALUE },
            new Content { Key = "ABOUT_TEXT", Value = DEFAULT_VALUE },
            new Content { Key = "ABOUT_BIRTHDATE", Value = "25/10/2004" },
            new Content { Key = "ABOUT_EMAIL", Value = DEFAULT_VALUE },
            new Content { Key = "ABOUT_EMAIL_LINK", Value = "mailto:maxmuster@example.com" },
            new Content { Key = "ABOUT_PHONE", Value = DEFAULT_VALUE },
            new Content { Key = "ABOUT_PHONE_LINK", Value = "tel:0123456789" },
            new Content { Key = "ABOUT_GITHUB", Value = DEFAULT_VALUE },
            new Content { Key = "ABOUT_GITHUB_LINK", Value = "https://github.com/maxmuster" },
            new Content { Key = "ABOUT_RESIDENCE", Value = DEFAULT_VALUE }
        );

        modelBuilder.Entity<Experience>().HasData(
            new Experience { Id = 1, Company = "Example", Text = DEFAULT_VALUE, StartDate = new DateOnly(2020, 8, 1), IsEducation = false },
            new Experience { Id = 2, Company = "Elpmaxe", Text = DEFAULT_VALUE, StartDate = new DateOnly(2016, 1, 1), EndDate = new DateOnly(2020, 7, 31), IsEducation = false }
        );

        modelBuilder.Entity<Hobby>().HasData(
            new Hobby { Id = 1, Text = DEFAULT_VALUE, Icon = "code" },
            new Hobby { Id = 2, Text = DEFAULT_VALUE, Icon = "code" }
        );

        modelBuilder.Entity<Reference>().HasData(
            new Reference { Id = 1, Title = "Max Mustermann", Employment = "Musterer", Text = DEFAULT_VALUE }
        );

        modelBuilder.Entity<Skill>().HasData(
            new Skill { Id = 1, Name = DEFAULT_VALUE, Level = 70 }
        );
    }
}