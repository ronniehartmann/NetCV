using Application.Dtos;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Services.Resources.Impl;

public class ExperienceService(
    IExperienceRepository experienceRepository,
    ILogger<ExperienceService> logger) : ResourceService<ExperienceDto>
{
    private readonly IExperienceRepository _experienceRepository = experienceRepository;
    private readonly ILogger<ExperienceService> _logger = logger;

    public override async Task<IEnumerable<ExperienceDto>> GetResourcesAsync()
    {
        var experiences = await _experienceRepository.GetAllExperiencesAsync();
        var experienceDtos = experiences.Select(ConvertToExperienceDto);
        return experienceDtos.OrderByDescending(e => e.StartDate);
    }

    public override async Task AddResourceAsync(ExperienceDto resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        var experience = new Experience
        {
            StartDate = resource.StartDate,
            EndDate = resource.EndDate,
            Company = resource.Company,
            CompanyLink = resource.CompanyLink,
            Text = resource.Text
        };

        await _experienceRepository.AddExperienceAsync(experience);
        _logger.LogInformation("Added experience with company '{}'", resource.Company);
    }

    public override async Task UpdateResourceAsync(ExperienceDto resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        var experience = await _experienceRepository.GetExperienceAsync(resource.Id)
            ?? throw new InvalidOperationException($"No experience with id '{resource.Id}' found");

        if (!resource.IsEqualToModel(experience))
        {
            experience.StartDate = resource.StartDate;
            experience.EndDate = resource.EndDate;
            experience.Company = resource.Company;
            experience.CompanyLink = resource.CompanyLink;
            experience.Text = resource.Text;

            await _experienceRepository.UpdateExperienceAsync(experience);
            _logger.LogInformation("Updated experience '{}'", resource.Id);
        }
    }

    public override async Task DeleteResourceAsync(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Provided id '{id}' is invalid.");
        }

        await _experienceRepository.DeleteExperienceAsync(id);
        _logger.LogInformation("Removed experience '{}'", id);
    }

    private static ExperienceDto ConvertToExperienceDto(Experience experience)
    {
        return new ExperienceDto
        {
            Id = experience.Id,
            StartDate = experience.StartDate,
            EndDate = experience.EndDate,
            Company = experience.Company,
            CompanyLink = experience.CompanyLink,
            Text = experience.Text
        };
    }
}
