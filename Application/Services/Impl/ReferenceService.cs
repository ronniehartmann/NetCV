using Application.Dtos;
using Domain.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Services.Impl;

public class ReferenceService(IServiceProvider serviceProvider, ILogger<ReferenceService> logger) : IReferenceService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<ReferenceService> _logger = logger;

    public async Task<IEnumerable<ReferenceDto>> GetReferencesAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var references = await context.References.ToListAsync();
        var referenceDtos = references.Select(ConvertToReferenceDto);
        return referenceDtos;
    }

    public async Task AddReferenceAsync(ReferenceDto reference)
    {
        ArgumentNullException.ThrowIfNull(reference);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var referenceModel = new Reference
        {
            Title = reference.Title,
            Employment = reference.Employment,
            Text = reference.Text
        };

        context.References.Add(referenceModel);
        await context.SaveChangesAsync();
        _logger.LogInformation("Added reference {}", reference.Title);
    }

    public async Task DeleteReferenceAsync(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Provided id {id} is invalid.");
        }

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var reference = await context.References.FindAsync(id);
        if (reference is null)
        {
            _logger.LogError("Couldn't find reference with id {}", id);
            throw new InvalidOperationException($"Couldn't find reference with id {id}.");
        }

        context.References.Remove(reference);
        await context.SaveChangesAsync();
        _logger.LogInformation("Removed reference {}", id);
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
