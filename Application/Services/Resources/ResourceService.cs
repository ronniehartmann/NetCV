namespace Application.Services.Resources;

public abstract class ResourceService<T>
{
    public abstract Task<IEnumerable<T>> GetResourcesAsync();

    public abstract Task AddResourceAsync(T resource);

    public abstract Task UpdateResourceAsync(T resource);

    public abstract Task DeleteResourceAsync(long id);
}
