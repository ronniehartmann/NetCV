using Domain.Models;

namespace Domain.Repositories;

public interface IEducationRepository
{
    Task<IEnumerable<Education>> GetAllEducationsAsync();

    Task<Education?> GetEducationAsync(long id);

    Task AddEducationAsync(Education education);

    Task UpdateEducationAsync(Education education);

    Task DeleteEducationAsync(long id);
}
