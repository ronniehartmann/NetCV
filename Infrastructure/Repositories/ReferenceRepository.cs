using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReferenceRepository(IDbContextFactory<CvContext> contextFactory) : IReferenceRepository
{
    private readonly IDbContextFactory<CvContext> _contextFactory = contextFactory;

    public async Task<IEnumerable<Reference>> GetAllReferencesAsync()
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var references = await context.References.ToListAsync();
        return references;
    }

    public async Task<Reference?> GetReferenceAsync(long id)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var reference = await context.References.FindAsync(id);
        return reference;
    }

    public async Task AddReferenceAsync(Reference reference)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        await context.References.AddAsync(reference);
        await context.SaveChangesAsync();
    }

    public async Task UpdateReferenceAsync(Reference reference)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var existingReference = await context.References.FindAsync(reference.Id)
            ?? throw new ArgumentException("Provided reference doesn't exist.");

        existingReference.Title = reference.Title;
        existingReference.Employment = reference.Employment;
        existingReference.Text = reference.Text;
        await context.SaveChangesAsync();
    }

    public async Task DeleteReferenceAsync(long id)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var reference = await context.References.FindAsync(id)
            ?? throw new ArgumentException($"Couldn't find reference with id '{id}'");

        context.References.Remove(reference);
        await context.SaveChangesAsync();
    }
}