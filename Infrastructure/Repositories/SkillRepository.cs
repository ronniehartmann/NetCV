using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SkillRepository(IDbContextFactory<CvContext> contextFactory) : ISkillRepository
{
    private readonly IDbContextFactory<CvContext> _contextFactory = contextFactory;

    public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var skills = await context.Skills.ToListAsync();
        return skills;
    }

    public async Task<Skill?> GetSkillAsync(long id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var skill = await context.Skills.FindAsync(id);
        return skill;
    }

    public async Task AddSkillAsync(Skill skill)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        await context.Skills.AddAsync(skill);
        await context.SaveChangesAsync();
    }

    public async Task UpdateSkillAsync(Skill skill)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var existingSkill = await context.Skills.FindAsync(skill.Id)
            ?? throw new ArgumentException("Provided skill doesn't exist.");

        existingSkill.Name = skill.Name;
        existingSkill.Level = skill.Level;
        await context.SaveChangesAsync();
    }

    public async Task DeleteSkillAsync(long id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var skill = await context.Skills.FindAsync(id)
            ?? throw new ArgumentException($"Couldn't find skill with id '{id}'");

        context.Skills.Remove(skill);
        await context.SaveChangesAsync();
    }
}