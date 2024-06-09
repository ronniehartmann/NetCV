using Application.Dtos;
using Domain.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Services.Resources.Impl;

public class EducationService(
    IServiceProvider serviceProvider,
    ILogger<EducationService> logger) : ResourceService<EducationDto>
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<EducationService> _logger = logger;

    public override async Task<IEnumerable<EducationDto>> GetResourcesAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var educations = await context.Educations.ToListAsync();
        var experienceDtos = educations.Select(ConvertToEducationDto);
        return experienceDtos.OrderByDescending(e => e.StartDate);
    }

    public override async Task AddResourceAsync(EducationDto resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var educationModel = new Education
        {
            School = resource.School,
            Title = resource.Title,
            StartDate = resource.StartDate,
            EndDate = resource.EndDate
        };

        context.Educations.Add(educationModel);
        await context.SaveChangesAsync();
        _logger.LogInformation("Added education with school '{}'", resource.School);
    }

    public override async Task UpdateResourceAsync(EducationDto resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var existingEducation = await context.Educations.FindAsync(resource.Id)
            ?? throw new InvalidOperationException($"No education with id '{resource.Id}' found");

        if (!resource.IsEqualToModel(existingEducation))
        {
            existingEducation.StartDate = resource.StartDate;
            existingEducation.EndDate = resource.EndDate;
            existingEducation.School = resource.School;
            existingEducation.Title = resource.Title;

            await context.SaveChangesAsync();
            _logger.LogInformation("Updated education '{}'", resource.Id);
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
