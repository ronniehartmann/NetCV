using Application.Dtos;
using Domain.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Services.Impl;

public class SkillService(IServiceProvider serviceProvider, ILogger<SkillService> logger) : ISkillService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<SkillService> _logger = logger;

    public async Task<IEnumerable<SkillDto>> GetSkillsAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var skills = await context.Skills.ToListAsync();
        var skillDtos = skills.Select(ConvertToSkillDto);
        return skillDtos;
    }

    public async Task AddSkillAsync(SkillDto skill)
    {
        ArgumentNullException.ThrowIfNull(skill);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var skillModel = new Skill
        {
            Name = skill.Name,
            Level = skill.Level
        };

        context.Skills.Add(skillModel);
        await context.SaveChangesAsync();
        _logger.LogInformation("Added skill {}", skill.Name);
    }

    public async Task DeleteSkillAsync(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Provided id {id} is invalid.");
        }

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var skill = await context.Skills.FindAsync(id);
        if (skill is null)
        {
            _logger.LogError("Couldn't find skill with id {}", id);
            throw new InvalidOperationException($"Couldn't find skill with id {id}.");
        }

        context.Skills.Remove(skill);
        await context.SaveChangesAsync();
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
