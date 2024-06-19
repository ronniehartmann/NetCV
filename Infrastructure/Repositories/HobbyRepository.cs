using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class HobbyRepository(IDbContextFactory<CvContext> contextFactory) : IHobbyRepository
{
    private readonly IDbContextFactory<CvContext> _contextFactory = contextFactory;

    public async Task<IEnumerable<Hobby>> GetAllHobbiesAsync()
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var hobbies = await context.Hobbies.ToListAsync();
        return hobbies;
    }

    public async Task<Hobby?> GetHobbyAsync(long id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var hobby = await context.Hobbies.FindAsync(id);
        return hobby;
    }

    public async Task AddHobbyAsync(Hobby hobby)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        await context.Hobbies.AddAsync(hobby);
        await context.SaveChangesAsync();
    }

    public async Task UpdateHobbyAsync(Hobby hobby)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var existingHobby = await context.Hobbies.FindAsync(hobby.Id)
            ?? throw new ArgumentException("Provided hobby doesn't exist.");

        existingHobby.Text = hobby.Text;
        existingHobby.Icon = hobby.Icon;
        await context.SaveChangesAsync();
    }

    public async Task DeleteHobbyAsync(long id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var hobby = await context.Hobbies.FindAsync(id)
            ?? throw new ArgumentException($"Couldn't find hobby with id '{id}'");

        context.Hobbies.Remove(hobby);
        await context.SaveChangesAsync();
    }
}