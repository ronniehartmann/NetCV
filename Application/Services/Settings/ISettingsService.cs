namespace Application.Services.Settings;

public interface ISettingsService
{
    Task<string> GetFavIconFileNameAsync();

    Task<string> GetPortraitFileNameAsync();

    Task<string> GetBackgroundFileNameAsync();

    Task<bool> GetShowHireMeAsync();

    Task<bool> GetShowFooterAsync();

    Task<bool> GetShowVersionAsync();

    Task<bool> GetShowPoweredByNetCvAsync();

    Task UpdateFavIconFileNameAsync(string fileName);

    Task UpdatePortraitFileNameAsync(string fileName);

    Task UpdateBackgroundFileNameAsync(string fileName);

    Task UpdateShowHireMeAsync(bool showHireMe);

    Task UpdateShowFooterAsync(bool showFooter);

    Task UpdateShowVersionAsync(bool showVersion);

    Task UpdateShowPoweredByNetCvAsync(bool showPoweredByNetCv);
}
