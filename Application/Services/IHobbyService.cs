using Application.Dtos;

namespace Application.Services;

public interface IHobbyService
{
    Task<IEnumerable<HobbyDto>> GetHobbiesAsync();

    Task AddOrUpdateHobbyAsync(HobbyDto hobby);

    Task DeleteHobbyAsync(long id);
}
