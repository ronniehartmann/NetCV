namespace Application.Services.Resources.Models;

public abstract class Resource<T>
{
    public abstract bool IsEqualToModel(T model);
}
