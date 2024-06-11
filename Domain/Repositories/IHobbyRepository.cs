using Domain.Models;

namespace Domain.Repositories;

public interface IHobbyRepository
{
    Task<IEnumerable<Hobby>> GetAllHobbiesAsync();

    Task<Hobby?> GetHobbyAsync(long id);

    Task AddHobbyAsync(Hobby hobby);

    Task UpdateHobbyAsync(Hobby hobby);

    Task DeleteHobbyAsync(long id);
}
