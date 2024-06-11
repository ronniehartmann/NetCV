namespace Application.Services.Contents;

public interface IContentService
{
    Task<IDictionary<string, string>> GetAllValuesAsync();

    Task<string> GetValueAsync(string key);

    Task SetValueAsync(string key, string value);

    Task RemoveValueAsync(string key);
}
