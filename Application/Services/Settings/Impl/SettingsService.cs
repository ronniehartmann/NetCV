
using Domain.Repositories;

namespace Application.Services.Settings.Impl;

public class SettingsService(ISettingsRepository settingsRepository) : ISettingsService
{
    private readonly ISettingsRepository _settingsRepository = settingsRepository;

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

    public async Task<bool> GetHideFooterAsync()
    {
        var settings = await _settingsRepository.GetSettingsAsync();
        return settings.HideFooter;
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

    public async Task UpdateHideFooterAsync(bool hideFooter)
    {
        var settings = await _settingsRepository.GetSettingsAsync();
        settings.HideFooter = hideFooter;
        await _settingsRepository.SetSettingsAsync(settings);
    }
}
