using Application.Dtos;

namespace Application.Services;

public interface IReferenceService
{
    Task<IEnumerable<ReferenceDto>> GetReferencesAsync();

    Task AddReferenceAsync(ReferenceDto reference);

    Task DeleteReferenceAsync(long id);
}
