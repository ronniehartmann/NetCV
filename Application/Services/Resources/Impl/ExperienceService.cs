using Application.Dtos;
using Domain.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Services.Resources.Impl;

public class ExperienceService(
    IServiceProvider serviceProvider,
    ILogger<ExperienceService> logger) : ResourceService<ExperienceDto>
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<ExperienceService> _logger = logger;

    public override async Task<IEnumerable<ExperienceDto>> GetResourcesAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var experiences = await context.Experiences.ToListAsync();
        var experienceDtos = experiences.Select(ConvertToExperienceDto);
        return experienceDtos.OrderByDescending(e => e.StartDate);
    }

    public override async Task AddResourceAsync(ExperienceDto resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var experienceModel = new Experience
        {
            StartDate = resource.StartDate,
            EndDate = resource.EndDate,
            Company = resource.Company,
            CompanyLink = resource.CompanyLink,
            Text = resource.Text
        };

        context.Experiences.Add(experienceModel);
        await context.SaveChangesAsync();
        _logger.LogInformation("Added experience with company '{}'", resource.Company);
    }

    public override async Task UpdateResourceAsync(ExperienceDto resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var existingExperience = await context.Experiences.FindAsync(resource.Id)
            ?? throw new InvalidOperationException($"No experience with id '{resource.Id}' found");

        if (!resource.IsEqualToModel(existingExperience))
        {
            existingExperience.StartDate = resource.StartDate;
            existingExperience.EndDate = resource.EndDate;
            existingExperience.Company = resource.Company;
            existingExperience.CompanyLink = resource.CompanyLink;
            existingExperience.Text = resource.Text;

            await context.SaveChangesAsync();
            _logger.LogInformation("Updated experience '{}'", resource.Id);
        }
    }

    public override async Task DeleteResourceAsync(long id)
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
