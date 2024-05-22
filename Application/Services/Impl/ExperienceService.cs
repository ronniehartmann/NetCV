using Application.Dtos;
using Domain.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services.Impl;

public class ExperienceService(CvContext context, ILogger<ExperienceService> logger) : IExperienceService
{
    private readonly CvContext _context = context;
    private readonly ILogger<ExperienceService> _logger = logger;

    public async Task<IEnumerable<ExperienceDto>> GetExperiencesAsync()
    {
        var experiences = await _context.Experiences.ToListAsync();
        var experienceDtos = experiences.Select(ConvertToExperienceDto);
        return experienceDtos;
    }

    public async Task AddExperienceAsync(AddExperienceDto experience)
    {
        ArgumentNullException.ThrowIfNull(experience);

        var experienceModel = new Experience
        {
            StartDate = experience.StartDate,
            EndDate = experience.EndDate,
            Company = experience.Company,
            Text = experience.Text
        };

        _context.Experiences.Add(experienceModel);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Added experience with company {}", experience.Company);
    }

    public async Task DeleteExperienceAsync(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Provided id {id} is invalid.");
        }

        var experience = await _context.Experiences.FindAsync(id);
        if (experience is null)
        {
            _logger.LogError("Couldn't find experience with id {}", id);
            throw new InvalidOperationException($"Couldn't find experience with id {id}.");
        }

        _context.Experiences.Remove(experience);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Removed experience {}", id);
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
