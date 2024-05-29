namespace Application.Services;

public interface IContentService
{
    Task<IDictionary<string, string>> GetAllValuesAsync();

    string GetValue(string key);

    Task<string> GetValueAsync(string key);

    bool TryGetValue(string key, out string value);

    void SetValue(string key, string value);

    Task SetValueAsync(string key, string value);

    void RemoveValue(string key);

    Task RemoveValueAsync(string key);
}
