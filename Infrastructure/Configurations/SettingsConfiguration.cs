using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SettingsConfiguration : IEntityTypeConfiguration<Settings>
{
    public void Configure(EntityTypeBuilder<Settings> builder)
    {
        builder.HasData(new Settings
        {
            Id = 1,
            FavIconFileName = "favicon.ico",
            PortraitImageFileName = "portrait.jpg",
            BackgroundImageFileName = "profile-background.jpg",
            ShowFooter = false,
            ShowVersion = true,
            ShowPoweredByNetCv = true
        });
    }
}
