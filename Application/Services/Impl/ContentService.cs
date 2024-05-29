using Domain.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Services.Impl;

public class ContentService(IServiceProvider serviceProvider, ILogger<ContentService> logger) : IContentService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<ContentService> _logger = logger;

    public async Task<IDictionary<string, string>> GetAllValuesAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var contents = await context.Contents.ToListAsync();
        return contents.ToDictionary(x => x.Key, x => x.Value);
    }

    public string GetValue(string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var content = context.Contents.Find(key);
        if (content is null)
        {
            _logger.LogError("Couldn't find element with key {}.", key);
            throw new InvalidOperationException($"Couldn't find element with key {key}.");
        }
        return content.Value;
    }

    public async Task<string> GetValueAsync(string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var content = await context.Contents.FindAsync(key);
        if (content is null)
        {
            _logger.LogError("Couldn't find element with key {}.", key);
            throw new InvalidOperationException($"Couldn't find element with key {key}.");
        }
        return content.Value;
    }

    public bool TryGetValue(string key, out string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        try
        {
            value = GetValue(key);
            return true;
        }
        catch
        {
            value = string.Empty;
            return false;
        }
    }

    public void SetValue(string key, string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentException.ThrowIfNullOrEmpty(value);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var existingContent = context.Contents.Find(key);
        if (existingContent is null)
        {
            var content = new Content
            {
                Key = key,
                Value = value
            };

            context.Contents.Add(content);
        }
        else
        {
            existingContent.Value = value;
            context.Contents.Update(existingContent);
        }

        context.SaveChanges();
    }

    public async Task SetValueAsync(string key, string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentException.ThrowIfNullOrEmpty(value);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var existingContent = await context.Contents.FindAsync(key);
        if (existingContent is null)
        {
            var content = new Content
            {
                Key = key,
                Value = value
            };

            context.Contents.Add(content);
        }
        else
        {
            existingContent.Value = value;
            context.Contents.Update(existingContent);
        }

        await context.SaveChangesAsync();
    }

    public void RemoveValue(string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var content = context.Contents.Find(key);
        if (content is null)
        {
            _logger.LogError("Couldn't find element with key {}.", key);
            throw new InvalidOperationException($"Couldn't find element with key {key}.");
        }

        context.Contents.Remove(content);
        context.SaveChanges();
        _logger.LogInformation("Removed content {}", key);
    }

    public async Task RemoveValueAsync(string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var content = await context.Contents.FindAsync(key);
        if (content is null)
        {
            _logger.LogError("Couldn't find element with key {}.", key);
            throw new InvalidOperationException($"Couldn't find element with key {key}.");
        }

        context.Contents.Remove(content);
        await context.SaveChangesAsync();
        _logger.LogInformation("Removed content {}", key);
    }
}
