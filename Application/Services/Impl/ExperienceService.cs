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
        return experienceDtos;
    }

    public async Task AddExperienceAsync(AddExperienceDto experience)
    {
        ArgumentNullException.ThrowIfNull(experience);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var experienceModel = new Experience
        {
            StartDate = experience.StartDate,
            EndDate = experience.EndDate,
            Company = experience.Company,
            Text = experience.Text
        };

        context.Experiences.Add(experienceModel);
        await context.SaveChangesAsync();
        _logger.LogInformation("Added experience with company '{}'", experience.Company);
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

    private static ExperienceDto ConvertToExperienceDto(Experience experience)
    {
        var duration = $"{experience.StartDate:MMMM yyyy} - ";
        if (experience.EndDate.HasValue)
        {
            duration += experience.EndDate.Value.ToString("MMMM yyyy");
        }
        else
        {
            duration += "Today";
        }

        return new ExperienceDto
        {
            Id = experience.Id,
            Company = experience.Company,
            Text = experience.Text,
            Duration = duration,
            IsActive = !experience.EndDate.HasValue
        };
    }
}
