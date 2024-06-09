using Application.Dtos;
using Domain.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Services.Resources.Impl;

public class ReferenceService(
    IServiceProvider serviceProvider,
    ILogger<ReferenceService> logger) : ResourceService<ReferenceDto>
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<ReferenceService> _logger = logger;

    public override async Task<IEnumerable<ReferenceDto>> GetResourcesAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var references = await context.References.ToListAsync();
        var referenceDtos = references.Select(ConvertToReferenceDto);
        return referenceDtos;
    }

    public override async Task AddResourceAsync(ReferenceDto resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var referenceModel = new Reference
        {
            Title = resource.Title,
            Employment = resource.Employment,
            Text = resource.Text
        };

        context.References.Add(referenceModel);
        await context.SaveChangesAsync();
        _logger.LogInformation("Added reference '{}'", resource.Title);
    }

    public override async Task UpdateResourceAsync(ReferenceDto resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var existingReference = await context.References.FindAsync(resource.Id)
            ?? throw new InvalidOperationException($"No reference with id '{resource.Id}' found");

        if (!resource.IsEqualToModel(existingReference))
        {
            existingReference.Title = resource.Title;
            existingReference.Employment = resource.Employment;
            existingReference.Text = resource.Text;

            await context.SaveChangesAsync();
            _logger.LogInformation("Updated reference '{}'", resource.Id);
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

        var reference = await context.References.FindAsync(id);
        if (reference is null)
        {
            _logger.LogError("Couldn't find reference with id '{}'", id);
            throw new InvalidOperationException($"Couldn't find reference with id '{id}'.");
        }

        context.References.Remove(reference);
        await context.SaveChangesAsync();
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
