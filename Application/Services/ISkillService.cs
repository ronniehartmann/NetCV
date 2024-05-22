using Application.Dtos;

namespace Application.Services;

public interface ISkillService
{
    Task<IEnumerable<SkillDto>> GetSkillsAsync();

    Task AddSkillAsync(SkillDto skill);

    Task DeleteSkillAsync(long id);
}
