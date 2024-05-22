using Domain.Models;
using Infrastructure;
using Microsoft.Extensions.Logging;

namespace Application.Services.Impl;

public class ContentService(CvContext context, ILogger<ContentService> logger) : IContentService
{
    private readonly CvContext _context = context;
    private readonly ILogger<ContentService> _logger = logger;

    public string GetValue(string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        var content = _context.Contents.Find(key);
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

        var content = await _context.Contents.FindAsync(key);
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

        var content = new Content
        {
            Key = key,
            Value = value
        };

        _context.Contents.Add(content);
        _context.SaveChanges();
    }

    public async Task SetValueAsync(string key, string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentException.ThrowIfNullOrEmpty(value);

        var content = new Content
        {
            Key = key,
            Value = value
        };

        _context.Contents.Add(content);
        await _context.SaveChangesAsync();
    }

    public void RemoveValue(string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        var content = _context.Contents.Find(key);
        if (content is null)
        {
            _logger.LogError("Couldn't find element with key {}.", key);
            throw new InvalidOperationException($"Couldn't find element with key {key}.");
        }

        _context.Contents.Remove(content);
        _context.SaveChanges();
        _logger.LogInformation("Removed content {}", key);
    }

    public async Task RemoveValueAsync(string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        var content = await _context.Contents.FindAsync(key);
        if (content is null)
        {
            _logger.LogError("Couldn't find element with key {}.", key);
            throw new InvalidOperationException($"Couldn't find element with key {key}.");
        }

        _context.Contents.Remove(content);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Removed content {}", key);
    }
}
