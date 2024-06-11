using Application.Dtos;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Services.Resources.Impl;

public class ReferenceService(
    IReferenceRepository referenceRepository,
    ILogger<ReferenceService> logger) : ResourceService<ReferenceDto>
{
    private readonly IReferenceRepository _referenceRepository = referenceRepository;
    private readonly ILogger<ReferenceService> _logger = logger;

    public override async Task<IEnumerable<ReferenceDto>> GetResourcesAsync()
    {
        var references = await _referenceRepository.GetAllReferencesAsync();
        var referenceDtos = references.Select(ConvertToReferenceDto);
        return referenceDtos;
    }

    public override async Task AddResourceAsync(ReferenceDto resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        var reference = new Reference
        {
            Title = resource.Title,
            Employment = resource.Employment,
            Text = resource.Text
        };

        await _referenceRepository.AddReferenceAsync(reference);
        _logger.LogInformation("Added reference '{}'", resource.Title);
    }

    public override async Task UpdateResourceAsync(ReferenceDto resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        var reference = await _referenceRepository.GetReferenceAsync(resource.Id)
            ?? throw new InvalidOperationException($"No reference with id '{resource.Id}' found");

        if (!resource.IsEqualToModel(reference))
        {
            reference.Title = resource.Title;
            reference.Employment = resource.Employment;
            reference.Text = resource.Text;

            await _referenceRepository.UpdateReferenceAsync(reference);
            _logger.LogInformation("Updated reference '{}'", resource.Id);
        }
    }

    public override async Task DeleteResourceAsync(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Provided id '{id}' is invalid.");
        }

        await _referenceRepository.DeleteReferenceAsync(id);
        _logger.LogInformation("Removed reference '{}'", id);
    }

    private static ReferenceDto ConvertToReferenceDto(Reference reference)
    {
        return new ReferenceDto
        {
            Id = reference.Id,
            Title = reference.Title,
            Employment = reference.Employment,
            Text = reference.Text
        };
    }
}
