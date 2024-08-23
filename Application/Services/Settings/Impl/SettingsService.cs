using Domain.Repositories;

namespace Application.Services.Settings.Impl;

public class SettingsService(ISettingsRepository settingsRepository) : ISettingsService
{
    private readonly ISettingsRepository _settingsRepository = settingsRepository;

    public async Task<string> GetFavIconFileNameAsync()
    {
        var settings = await _settingsRepository.GetSettingsAsync();
        return settings.FavIconFileName;
    }

    public async Task<string> GetPortraitFileNameAsync()
    {
        var settings = await _settingsRepository.GetSettingsAsync();
        return settings.PortraitImageFileName;
    }

    public async Task<string> GetBackgroundFileNameAsync()
    {
        var settings = await _settingsRepository.GetSettingsAsync();
        return settings.BackgroundImageFileName;
    }

    public async Task<bool> GetShowHireMeAsync()
    {
        var settings = await _settingsRepository.GetSettingsAsync();
        return settings.ShowHireMe;
    }

    public async Task<bool> GetShowFooterAsync()
    {
        var settings = await _settingsRepository.GetSettingsAsync();
        return settings.ShowFooter;
    }

    public async Task<bool> GetShowVersionAsync()
    {
        var settings = await _settingsRepository.GetSettingsAsync();
        return settings.ShowVersion;
    }

    public async Task<bool> GetShowPoweredByNetCvAsync()
    {
        var settings = await _settingsRepository.GetSettingsAsync();
        return settings.ShowPoweredByNetCv;
    }

    public async Task UpdateFavIconFileNameAsync(string fileName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);

        var settings = await _settingsRepository.GetSettingsAsync();
        settings.FavIconFileName = fileName;
        await _settingsRepository.SetSettingsAsync(settings);
    }

    public async Task UpdatePortraitFileNameAsync(string fileName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);

        var settings = await _settingsRepository.GetSettingsAsync();
        settings.PortraitImageFileName = fileName;
        await _settingsRepository.SetSettingsAsync(settings);
    }

    public async Task UpdateBackgroundFileNameAsync(string fileName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);

        var settings = await _settingsRepository.GetSettingsAsync();
        settings.BackgroundImageFileName = fileName;
        await _settingsRepository.SetSettingsAsync(settings);
    }

    public async Task UpdateShowHireMeAsync(bool showHireMe)
    {
        var settings = await _settingsRepository.GetSettingsAsync();
        settings.ShowHireMe = showHireMe;
        await _settingsRepository.SetSettingsAsync(settings);
    }

    public async Task UpdateShowFooterAsync(bool showFooter)
    {
        var settings = await _settingsRepository.GetSettingsAsync();
        settings.ShowFooter = showFooter;
        await _settingsRepository.SetSettingsAsync(settings);
    }

    public async Task UpdateShowVersionAsync(bool showVersion)
    {
        var settings = await _settingsRepository.GetSettingsAsync();
        settings.ShowVersion = showVersion;
        await _settingsRepository.SetSettingsAsync(settings);
    }

    public async Task UpdateShowPoweredByNetCvAsync(bool showPoweredByNetCv)
    {
        var settings = await _settingsRepository.GetSettingsAsync();
        settings.ShowPoweredByNetCv = showPoweredByNetCv;
        await _settingsRepository.SetSettingsAsync(settings);
    }
}
