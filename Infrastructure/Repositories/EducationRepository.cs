using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EducationRepository(IDbContextFactory<CvContext> contextFactory) : IEducationRepository
{
    private readonly IDbContextFactory<CvContext> _contextFactory = contextFactory;

    public async Task<IEnumerable<Education>> GetAllEducationsAsync()
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var educations = await context.Educations.ToListAsync();
        return educations;
    }

    public async Task<Education?> GetEducationAsync(long id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var education = await context.Educations.FindAsync(id);
        return education;
    }

    public async Task AddEducationAsync(Education education)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        await context.Educations.AddAsync(education);
        await context.SaveChangesAsync();
    }

    public async Task UpdateEducationAsync(Education education)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var existingEducation = await context.Educations.FindAsync(education.Id)
            ?? throw new ArgumentException("Provided education doesn't exist.");

        existingEducation.School = education.School;
        existingEducation.Title = education.Title;
        existingEducation.StartDate = education.StartDate;
        existingEducation.EndDate = education.EndDate;
        await context.SaveChangesAsync();
    }

    public async Task DeleteEducationAsync(long id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var education = await context.Educations.FindAsync(id)
            ?? throw new ArgumentException($"Couldn't find education with id '{id}'");

        context.Educations.Remove(education);
        await context.SaveChangesAsync();
    }
}