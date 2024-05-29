using Application.Dtos;

namespace Application.Services;

public interface IExperienceService
{
    Task<IEnumerable<ExperienceDto>> GetExperiencesAsync();

    Task AddExperienceAsync(AddExperienceDto experience);

    Task DeleteExperienceAsync(long id);
}
