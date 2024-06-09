using Application.Dtos;

namespace Application.Services;

public interface IEducationService
{
    Task<IEnumerable<EducationDto>> GetEducationsAsync();

    Task AddEducationAsync(EducationDto education);

    Task UpdateEducationAsync(EducationDto education);

    Task DeleteEducationAsync(long id);
}
