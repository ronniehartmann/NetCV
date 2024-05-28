using Application.Dtos;
using Domain.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Services.Impl;

public class HobbyService(IServiceProvider serviceProvider, ILogger<HobbyService> logger) : IHobbyService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<HobbyService> _logger = logger;

    public async Task<IEnumerable<HobbyDto>> GetHobbiesAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var hobbies = await context.Hobbies.ToListAsync();
        var hobbyDtos = hobbies.Select(ConvertToHobbyDto);
        return hobbyDtos;
    }

    public async Task AddHobbyAsync(HobbyDto hobby)
    {
        ArgumentNullException.ThrowIfNull(hobby);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var hobbyModel = new Hobby
        {
            Text = hobby.Name,
            Icon = hobby.Icon
        };

        context.Hobbies.Add(hobbyModel);
        await context.SaveChangesAsync();
        _logger.LogInformation("Added hobby {}", hobby.Name);
    }

    public async Task DeleteHobbyAsync(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Provided id {id} is invalid.");
        }

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var hobby = await context.Hobbies.FindAsync(id);
        if (hobby is null)
        {
            _logger.LogError("Couldn't find hobby with id {}", id);
            throw new InvalidOperationException($"Couldn't find hobby with id {id}.");
        }

        context.Hobbies.Remove(hobby);
        await context.SaveChangesAsync();
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
