using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Services.Contents.Impl;

public class ContentService(IContentRepository contentRepository, ILogger<ContentService> logger) : IContentService
{
    private readonly IContentRepository _contentRepository = contentRepository;
    private readonly ILogger<ContentService> _logger = logger;

    public async Task<IDictionary<string, string>> GetAllValuesAsync()
    {
        var contents = await _contentRepository.GetAllContentsAsync();
        return contents.ToDictionary(x => x.Key, x => x.Value);
    }

    public async Task<string> GetValueAsync(string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        var content = await _contentRepository.GetContentAsync(key);
        if (content is null)
        {
            _logger.LogError("Couldn't find content with key {}.", key);
            throw new InvalidOperationException($"Couldn't find content with key {key}.");
        }
        return content.Value;
    }

    public async Task SetValueAsync(string key, string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentException.ThrowIfNullOrEmpty(value);

        var content = await _contentRepository.GetContentAsync(key);
        if (content is null)
        {
            content = new Content
            {
                Key = key,
                Value = value
            };

            await _contentRepository.AddContentAsync(content);
            _logger.LogInformation("Added new content '{}'", key);
        }
        else
        {
            content.Value = value;
            await _contentRepository.UpdateContentAsync(content);
            _logger.LogInformation("Updated value of content '{}' to '{}'", key, value);
        }
    }

    public async Task RemoveValueAsync(string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        await _contentRepository.DeleteContentAsync(key);
        _logger.LogInformation("Removed content '{}'", key);
    }
}
