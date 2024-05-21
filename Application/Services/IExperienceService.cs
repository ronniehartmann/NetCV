using Application.Dtos;

namespace Application.Services;

public interface IExperienceService
{
    Task<IEnumerable<ExperienceDto>> GetExperiencesAsync();

    Task AddExperienceAsync(ExperienceDto experience);

    Task DeleteExperienceAsync(long id);
}
