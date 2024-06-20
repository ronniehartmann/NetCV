using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SettingsRepository(IDbContextFactory<CvContext> contextFactory) : ISettingsRepository
{
    private readonly IDbContextFactory<CvContext> _contextFactory = contextFactory;

    public async Task<Settings> GetSettingsAsync()
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var settings = await context.Settings.FirstOrDefaultAsync();

        if (settings is null)
        {
            settings = new Settings
            {
                FavIconFileName = "favicon.ico",
                BackgroundImageFileName = "profile-background.jpg",
                PortraitImageFileName = "portrait.jpg",
                ShowFooter = false,
                ShowVersion = true,
                ShowPoweredByNetCv = true
            };

            await SetSettingsAsync(settings);
        }

        return settings;
    }

    public async Task SetSettingsAsync(Settings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentException.ThrowIfNullOrWhiteSpace(settings.FavIconFileName);
        ArgumentException.ThrowIfNullOrWhiteSpace(settings.PortraitImageFileName);
        ArgumentException.ThrowIfNullOrWhiteSpace(settings.BackgroundImageFileName);

        using var context = await _contextFactory.CreateDbContextAsync();
        var existingSettings = await context.Settings.FirstOrDefaultAsync();

        if (existingSettings is null)
        {
            context.Settings.Add(settings);
        }
        else
        {
            existingSettings.FavIconFileName = settings.FavIconFileName;
            existingSettings.PortraitImageFileName = settings.PortraitImageFileName;
            existingSettings.BackgroundImageFileName = settings.BackgroundImageFileName;
            existingSettings.ShowFooter = settings.ShowFooter;
            existingSettings.ShowVersion = settings.ShowVersion;
            existingSettings.ShowPoweredByNetCv = settings.ShowPoweredByNetCv;
        }

        await context.SaveChangesAsync();
    }
}
