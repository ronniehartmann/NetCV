namespace Application.Services.Resources;

public abstract class ResourceService<T>
{
    public abstract Task<IEnumerable<T>> GetResourcesAsync();

    public virtual Task AddResourceAsync(T resource)
    {
        OnDbChange?.Invoke();
        return Task.CompletedTask;
    }

    public virtual Task UpdateResourceAsync(T resource)
    {
        OnDbChange?.Invoke();
        return Task.CompletedTask;
    }

    public virtual Task DeleteResourceAsync(long id)
    {
        OnDbChange?.Invoke();
        return Task.CompletedTask;
    }

    public virtual Action? OnDbChange { get; set; }
}
