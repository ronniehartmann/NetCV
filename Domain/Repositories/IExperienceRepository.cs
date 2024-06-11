using Domain.Models;

namespace Domain.Repositories;

public interface IExperienceRepository
{
    Task<IEnumerable<Experience>> GetAllExperiencesAsync();

    Task<Experience?> GetExperienceAsync(long id);

    Task AddExperienceAsync(Experience experience);

    Task UpdateExperienceAsync(Experience experience);

    Task DeleteExperienceAsync(long id);
}
