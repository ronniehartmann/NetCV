namespace Application.Services.Settings;

public interface ISettingsService
{
    Task<string> GetPortraitFileNameAsync();

    Task<string> GetBackgroundFileNameAsync();

    Task<bool> GetShowFooterAsync();

    Task<bool> GetShowVersionAsync();

    Task<bool> GetShowPoweredByNetCvAsync();

    Task UpdatePortraitFileNameAsync(string fileName);

    Task UpdateBackgroundFileNameAsync(string fileName);

    Task UpdateShowFooterAsync(bool showFooter);

    Task UpdateShowVersionAsync(bool showVersion);

    Task UpdateShowPoweredByNetCvAsync(bool showPoweredByNetCv);
}
