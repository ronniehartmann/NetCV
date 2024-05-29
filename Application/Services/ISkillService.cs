using Application.Dtos;

namespace Application.Services;

public interface ISkillService
{
    Task<IEnumerable<SkillDto>> GetSkillsAsync();

    Task AddOrUpdateSkillAsync(SkillDto skill);

    Task DeleteSkillAsync(long id);
}
