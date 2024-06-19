using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ExperienceRepository(IDbContextFactory<CvContext> contextFactory) : IExperienceRepository
{
    private readonly IDbContextFactory<CvContext> _contextFactory = contextFactory;

    public async Task<IEnumerable<Experience>> GetAllExperiencesAsync()
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var experiences = await context.Experiences.ToListAsync();
        return experiences;
    }

    public async Task<Experience?> GetExperienceAsync(long id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var experience = await context.Experiences.FindAsync(id);
        return experience;
    }

    public async Task AddExperienceAsync(Experience experience)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        await context.Experiences.AddAsync(experience);
        await context.SaveChangesAsync();
    }

    public async Task UpdateExperienceAsync(Experience experience)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var existingExperience = await context.Experiences.FindAsync(experience.Id)
            ?? throw new ArgumentException("Provided experience doesn't exist.");

        existingExperience.StartDate = experience.StartDate;
        existingExperience.EndDate = experience.EndDate;
        existingExperience.Company = experience.Company;
        existingExperience.CompanyLink = experience.CompanyLink;
        existingExperience.Text = experience.Text;
        await context.SaveChangesAsync();
    }

    public async Task DeleteExperienceAsync(long id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var experience = await context.Experiences.FindAsync(id)
            ?? throw new ArgumentException($"Couldn't find experience with id '{id}'");

        context.Experiences.Remove(experience);
        await context.SaveChangesAsync();
    }
}