using Domain.Models;

namespace Domain.Repositories;

public interface IContentRepository
{
    Task<IEnumerable<Content>> GetAllContentsAsync();

    Task<Content?> GetContentAsync(string key);

    Task AddContentAsync(Content content);

    Task UpdateContentAsync(Content content);

    Task DeleteContentAsync(string key);
}
