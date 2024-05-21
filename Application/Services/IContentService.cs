namespace Application.Services;

public interface IContentService
{
    string GetValue(string key);

    Task<string> GetValueAsync(string key);

    bool TryGetValue(string key, out string value);

    void SetValue(string key, string value);

    Task SetValueAsync(string key, string value);
}
