namespace Application.Services.Settings;

public interface ISettingsService
{
    Task<string> GetPortraitFileNameAsync();

    Task<string> GetBackgroundFileNameAsync();

    Task UpdatePortraitFileNameAsync(string fileName);

    Task UpdateBackgroundFileNameAsync(string fileName);
}
