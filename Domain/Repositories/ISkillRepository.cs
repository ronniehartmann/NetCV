using Domain.Models;

namespace Domain.Repositories;

public interface ISkillRepository
{
    Task<IEnumerable<Skill>> GetAllSkillsAsync();

    Task<Skill?> GetSkillAsync(long id);

    Task AddSkillAsync(Skill skill);

    Task UpdateSkillAsync(Skill skill);

    Task DeleteSkillAsync(long id);
}
