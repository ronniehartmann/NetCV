using Application.Dtos;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Services.Resources.Impl;

public class EducationService(
    IEducationRepository educationRepository,
    ILogger<EducationService> logger) : ResourceService<EducationDto>
{
    private readonly IEducationRepository _educationRepository = educationRepository;
    private readonly ILogger<EducationService> _logger = logger;

    public override async Task<IEnumerable<EducationDto>> GetResourcesAsync()
    {
        var educations = await _educationRepository.GetAllEducationsAsync();
        var experienceDtos = educations.Select(ConvertToEducationDto);
        return experienceDtos.OrderByDescending(e => e.StartDate);
    }

    public override async Task AddResourceAsync(EducationDto resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        var education = new Education
        {
            School = resource.School,
            Title = resource.Title,
            StartDate = resource.StartDate,
            EndDate = resource.EndDate
        };

        await _educationRepository.AddEducationAsync(education);
        _logger.LogInformation("Added education with school '{}'", resource.School);
    }

    public override async Task UpdateResourceAsync(EducationDto resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        var education = await _educationRepository.GetEducationAsync(resource.Id)
            ?? throw new InvalidOperationException($"No education with id '{resource.Id}' found");

        if (!resource.IsEqualToModel(education))
        {
            education.StartDate = resource.StartDate;
            education.EndDate = resource.EndDate;
            education.School = resource.School;
            education.Title = resource.Title;

            await _educationRepository.UpdateEducationAsync(education);
            _logger.LogInformation("Updated education '{}'", resource.Id);
        }
    }

    public override async Task DeleteResourceAsync(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Provided id '{id}' is invalid.");
        }

        await _educationRepository.DeleteEducationAsync(id);
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
