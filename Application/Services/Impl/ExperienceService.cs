using Application.Dtos;
using Domain.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Services.Impl;

public class ExperienceService(IServiceProvider serviceProvider, ILogger<ExperienceService> logger) : IExperienceService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<ExperienceService> _logger = logger;

    public async Task<IEnumerable<ExperienceDto>> GetExperiencesAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var experiences = await context.Experiences.ToListAsync();
        var experienceDtos = experiences.Select(ConvertToExperienceDto);
        return experienceDtos.OrderByDescending(e => e.StartDate);
    }

    public async Task AddExperienceAsync(ExperienceDto experience)
    {
        ArgumentNullException.ThrowIfNull(experience);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var experienceModel = new Experience
        {
            StartDate = experience.StartDate,
            EndDate = experience.EndDate,
            Company = experience.Company,
            CompanyLink = experience.CompanyLink,
            Text = experience.Text
        };

        context.Experiences.Add(experienceModel);
        await context.SaveChangesAsync();
        _logger.LogInformation("Added experience with company '{}'", experience.Company);
    }

    public async Task UpdateExperienceAsync(ExperienceDto experience)
    {
        ArgumentNullException.ThrowIfNull(experience);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var existingExperience = await context.Experiences.FindAsync(experience.Id)
            ?? throw new InvalidOperationException($"No experience with id '{experience.Id}' found");

        if (HasChanges(existingExperience, experience))
        {
            existingExperience.StartDate = experience.StartDate;
            existingExperience.EndDate = experience.EndDate;
            existingExperience.Company = experience.Company;
            existingExperience.CompanyLink = experience.CompanyLink;
            existingExperience.Text = experience.Text;

            await context.SaveChangesAsync();
            _logger.LogInformation("Updated experience '{}'", experience.Id);
        }
    }

    public async Task DeleteExperienceAsync(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Provided id '{id}' is invalid.");
        }

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var experience = await context.Experiences.FindAsync(id);
        if (experience is null)
        {
            _logger.LogError("Couldn't find experience with id '{}'", id);
            throw new InvalidOperationException($"Couldn't find experience with id '{id}'.");
        }

        context.Experiences.Remove(experience);
        await context.SaveChangesAsync();
        _logger.LogInformation("Removed experience '{}'", id);
    }

    private static bool HasChanges(Experience experience, ExperienceDto experienceDto)
    {
        return experience.StartDate != experienceDto.StartDate
            || experience.EndDate != experienceDto.EndDate
            || !string.Equals(experience.Company, experienceDto.Company)
            || !string.Equals(experience.CompanyLink, experienceDto.CompanyLink)
            || !string.Equals(experience.Text, experienceDto.Text);
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
