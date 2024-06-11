using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ContentRepository(IDbContextFactory<CvContext> contextFactory) : IContentRepository
{
    private readonly IDbContextFactory<CvContext> _contextFactory = contextFactory;

    public async Task<IEnumerable<Content>> GetAllContentsAsync()
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var contents = await context.Contents.ToListAsync();
        return contents;
    }

    public async Task<Content?> GetContentAsync(string key)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var content = await context.Contents.FindAsync(key);
        return content;
    }

    public async Task AddContentAsync(Content content)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        await context.Contents.AddAsync(content);
        await context.SaveChangesAsync();
    }

    public async Task UpdateContentAsync(Content content)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var existingContent = await context.Contents.FindAsync(content.Key)
            ?? throw new ArgumentException("Provided content doesn't exist.");

        existingContent.Value = content.Value;
        await context.SaveChangesAsync();
    }

    public async Task DeleteContentAsync(string key)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var content = await context.Contents.FindAsync(key)
            ?? throw new ArgumentException($"Couldn't find content with key '{key}'");

        context.Contents.Remove(content);
        await context.SaveChangesAsync();
    }
}