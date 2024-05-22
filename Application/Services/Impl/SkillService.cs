using Application.Dtos;
using Domain.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services.Impl;

public class SkillService(CvContext context, ILogger<SkillService> logger) : ISkillService
{
    private readonly CvContext _context = context;
    private readonly ILogger<SkillService> _logger = logger;

    public async Task<IEnumerable<SkillDto>> GetSkillsAsync()
    {
        var skills = await _context.Skills.ToListAsync();
        var skillDtos = skills.Select(ConvertToSkillDto);
        return skillDtos;
    }

    public async Task AddSkillAsync(SkillDto skill)
    {
        ArgumentNullException.ThrowIfNull(skill);

        var skillModel = new Skill
        {
            Name = skill.Name,
            Level = skill.Level
        };

        _context.Skills.Add(skillModel);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Added skill {}", skill.Name);
    }

    public async Task DeleteSkillAsync(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Provided id {id} is invalid.");
        }

        var skill = await _context.Skills.FindAsync(id);
        if (skill is null)
        {
            _logger.LogError("Couldn't find skill with id {}", id);
            throw new InvalidOperationException($"Couldn't find skill with id {id}.");
        }

        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Removed skill {}", id);
    }

    private static SkillDto ConvertToSkillDto(Skill skill)
    {
        return new SkillDto
        {
            Id = skill.Id,
            Name = skill.Name,
            Level = skill.Level
        };
    }
}
