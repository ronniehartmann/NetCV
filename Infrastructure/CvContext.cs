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
        Seed(modelBuilder);
    }

    private void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Content>().HasData(
            new Content { Id = 1, Key = "PROFILE_FULLNAME", Value = DEFAULT_VALUE },
            new Content { Id = 2, Key = "PROFILE_EMPLOYMENT", Value = DEFAULT_VALUE },
            new Content { Id = 3, Key = "ABOUT_TEXT", Value = DEFAULT_VALUE },
            new Content { Id = 4, Key = "ABOUT_BIRTH_DATE", Value = "25/10/2004" },
            new Content { Id = 5, Key = "ABOUT_EMAIL", Value = DEFAULT_VALUE },
            new Content { Id = 6, Key = "ABOUT_EMAIL_LINK", Value = "mailto:maxmuster@example.com" },
            new Content { Id = 7, Key = "ABOUT_PHONE", Value = DEFAULT_VALUE },
            new Content { Id = 8, Key = "ABOUT_PHONE_LINK", Value = "tel:0123456789" },
            new Content { Id = 9, Key = "ABOUT_GITHUB", Value = DEFAULT_VALUE },
            new Content { Id = 10, Key = "ABOUT_GITHUB_LINK", Value = "https://github.com/maxmuster" },
            new Content { Id = 11, Key = "ABOUT_RESIDENCE", Value = DEFAULT_VALUE }
        );

        modelBuilder.Entity<Hobby>().HasData(
            new Hobby { Id = 1, Text = DEFAULT_VALUE, Icon = "code" },
            new Hobby { Id = 2, Text = DEFAULT_VALUE, Icon = "code" }
        );

        modelBuilder.Entity<Skill>().HasData(
            new Skill { Id = 1, Name = DEFAULT_VALUE, Level = 70 }
        );

        modelBuilder.Entity<Experience>().HasData(
            new Experience { Id = 1, Company = "Example", Text = DEFAULT_VALUE, StartDate = new DateOnly(2020, 8, 1) },
            new Experience { Id = 2, Company = "Elpmaxe", Text = DEFAULT_VALUE, StartDate = new DateOnly(2016, 1, 1), EndDate = new DateOnly(2020, 7, 31) }
        );
    }
}