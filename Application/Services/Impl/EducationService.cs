using Application.Dtos;
using Domain.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Services.Impl;

public class EducationService(IServiceProvider serviceProvider, ILogger<EducationService> logger) : IEducationService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<EducationService> _logger = logger;

    public async Task<IEnumerable<EducationDto>> GetEducationsAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var educations = await context.Educations.ToListAsync();
        var experienceDtos = educations.Select(ConvertToEducationDto);
        return experienceDtos.OrderByDescending(e => e.StartDate);
    }

    public async Task AddEducationAsync(EducationDto education)
    {
        ArgumentNullException.ThrowIfNull(education);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var educationModel = new Education
        {
            School = education.School,
            Title = education.Title,
            StartDate = education.StartDate,
            EndDate = education.EndDate
        };

        context.Educations.Add(educationModel);
        await context.SaveChangesAsync();
        _logger.LogInformation("Added education with school '{}'", education.School);
    }

    public async Task UpdateEducationAsync(EducationDto education)
    {
        ArgumentNullException.ThrowIfNull(education);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var existingEducation = await context.Educations.FindAsync(education.Id)
            ?? throw new InvalidOperationException($"No education with id '{education.Id}' found");

        if (HasChanges(existingEducation, education))
        {
            existingEducation.StartDate = education.StartDate;
            existingEducation.EndDate = education.EndDate;
            existingEducation.School = education.School;
            existingEducation.Title = education.Title;

            await context.SaveChangesAsync();
            _logger.LogInformation("Updated education '{}'", education.Id);
        }
    }

    public async Task DeleteEducationAsync(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Provided id '{id}' is invalid.");
        }

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var education = await context.Educations.FindAsync(id);
        if (education is null)
        {
            _logger.LogError("Couldn't find education with id '{}'", id);
            throw new InvalidOperationException($"Couldn't find education with id '{id}'.");
        }

        context.Educations.Remove(education);
        await context.SaveChangesAsync();
        _logger.LogInformation("Removed education '{}'", id);
    }

    private static bool HasChanges(Education education, EducationDto educationDto)
    {
        return education.StartDate != educationDto.StartDate
            || education.EndDate != educationDto.EndDate
            || !string.Equals(education.School, educationDto.School)
            || !string.Equals(education.Title, educationDto.Title);
    }

    private static EducationDto ConvertToEducationDto(Education education)
    {
        return new EducationDto
        {
            Id = education.Id,
            School = education.School,
            Title = education.Title,
            StartDate = education.StartDate,
            EndDate = education.EndDate
        };
    }
}
