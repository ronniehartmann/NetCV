using Application.Dtos;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Services.Resources.Impl;

public class SkillService(
    ISkillRepository skillRepository, 
    ILogger<SkillService> logger) : ResourceService<SkillDto>
{
    private readonly ISkillRepository _skillRepository = skillRepository;
    private readonly ILogger<SkillService> _logger = logger;

    public override async Task<IEnumerable<SkillDto>> GetResourcesAsync()
    {
        var skills = await _skillRepository.GetAllSkillsAsync();
        var skillDtos = skills.Select(ConvertToSkillDto);
        return skillDtos;
    }

    public override async Task AddResourceAsync(SkillDto resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        var skill = new Skill
        {
            Name = resource.Name,
            Level = resource.Level
        };

        await _skillRepository.AddSkillAsync(skill);
        _logger.LogInformation("Added skill '{}'", resource.Name);
    }

    public override async Task UpdateResourceAsync(SkillDto resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        var skill = await _skillRepository.GetSkillAsync(resource.Id)
            ?? throw new InvalidOperationException($"No skill with id '{resource.Id}' found");

        if (!resource.IsEqualToModel(skill))
        {
            skill.Name = resource.Name;
            skill.Level = resource.Level;

            await _skillRepository.UpdateSkillAsync(skill);
            _logger.LogInformation("Updated skill '{}'", resource.Id);
        }
    }

    public override async Task DeleteResourceAsync(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Provided id '{id}' is invalid.");
        }

        await _skillRepository.DeleteSkillAsync(id);
        _logger.LogInformation("Removed skill '{}'", id);
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
