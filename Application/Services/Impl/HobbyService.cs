using Application.Dtos;
using Domain.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services.Impl;

public class HobbyService(CvContext context, ILogger<HobbyService> logger) : IHobbyService
{
    private readonly CvContext _context = context;
    private readonly ILogger<HobbyService> _logger = logger;

    public async Task<IEnumerable<HobbyDto>> GetHobbiesAsync()
    {
        var hobbies = await _context.Hobbies.ToListAsync();
        var hobbyDtos = hobbies.Select(ConvertToHobbyDto);
        return hobbyDtos;
    }

    public async Task AddHobbyAsync(HobbyDto hobby)
    {
        ArgumentNullException.ThrowIfNull(hobby);

        var hobbyModel = new Hobby
        {
            Text = hobby.Name,
            Icon = hobby.Icon
        };

        _context.Hobbies.Add(hobbyModel);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Added hobby {}", hobby.Name);
    }

    public async Task DeleteHobbyAsync(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Provided id {id} is invalid.");
        }

        var hobby = await _context.Hobbies.FindAsync(id);
        if (hobby is null)
        {
            _logger.LogError("Couldn't find hobby with id {}", id);
            throw new InvalidOperationException($"Couldn't find hobby with id {id}.");
        }

        _context.Hobbies.Remove(hobby);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Removed hobby {}", id);
    }

    private static HobbyDto ConvertToHobbyDto(Hobby hobby)
    {
        return new HobbyDto
        {
            Id = hobby.Id,
            Name = hobby.Text,
            Icon = hobby.Icon
        };
    }
}
