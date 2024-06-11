using Domain.Models;

namespace Domain.Repositories;

public interface IReferenceRepository
{
    Task<IEnumerable<Reference>> GetAllReferencesAsync();

    Task<Reference?> GetReferenceAsync(long id);

    Task AddReferenceAsync(Reference reference);

    Task UpdateReferenceAsync(Reference reference);

    Task DeleteReferenceAsync(long id);
}
