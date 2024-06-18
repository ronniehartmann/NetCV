using Domain.Models;

namespace Domain.Repositories;

public interface ISettingsRepository
{
    Task<Settings> GetSettingsAsync();

    Task SetSettingsAsync(Settings settings);
}
