namespace Application.Services.Settings;

public interface ISettingsService
{
    Task<string> GetPortraitFileNameAsync();

    Task<string> GetBackgroundFileNameAsync();

    Task<bool> GetHideFooterAsync();

    Task UpdatePortraitFileNameAsync(string fileName);

    Task UpdateBackgroundFileNameAsync(string fileName);

    Task UpdateHideFooterAsync(bool hideFooter);
}
