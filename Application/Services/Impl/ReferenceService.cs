using Application.Dtos;
using Domain.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services.Impl;

public class ReferenceService(CvContext context, ILogger<ReferenceService> logger) : IReferenceService
{
    private readonly CvContext _context = context;
    private readonly ILogger<ReferenceService> _logger = logger;

    public async Task<IEnumerable<ReferenceDto>> GetReferencesAsync()
    {
        var references = await _context.References.ToListAsync();
        var referenceDtos = references.Select(ConvertToReferenceDto);
        return referenceDtos;
    }

    public async Task AddReferenceAsync(ReferenceDto reference)
    {
        ArgumentNullException.ThrowIfNull(reference);

        var referenceModel = new Reference
        {
            Title = reference.Title,
            Employment = reference.Employment,
            Text = reference.Text
        };

        _context.References.Add(referenceModel);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Added reference {}", reference.Title);
    }

    public async Task DeleteReferenceAsync(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Provided id {id} is invalid.");
        }

        var reference = await _context.References.FindAsync(id);
        if (reference is null)
        {
            _logger.LogError("Couldn't find reference with id {}", id);
            throw new InvalidOperationException($"Couldn't find reference with id {id}.");
        }

        _context.References.Remove(reference);
        await _context.SaveChangesAsync();
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
