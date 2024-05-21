using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public partial class CvContext : DbContext
{
    public CvContext(DbContextOptions<CvContext> options)
            : base(options)
    {
    }

    public DbSet<TextEntry> TextEntries { get; set; }
}