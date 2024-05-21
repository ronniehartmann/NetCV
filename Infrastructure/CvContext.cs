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
}