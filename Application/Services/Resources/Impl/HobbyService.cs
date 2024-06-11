using Application.Dtos;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Services.Resources.Impl;

public class HobbyService(
    IHobbyRepository hobbyRepository,
    ILogger<HobbyService> logger) : ResourceService<HobbyDto>
{
    private readonly IHobbyRepository _hobbyRepository = hobbyRepository;
    private readonly ILogger<HobbyService> _logger = logger;

    public override async Task<IEnumerable<HobbyDto>> GetResourcesAsync()
    {
        var hobbies = await _hobbyRepository.GetAllHobbiesAsync();
        var hobbyDtos = hobbies.Select(ConvertToHobbyDto);
        return hobbyDtos;
    }

    public override async Task AddResourceAsync(HobbyDto resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        var hobby = new Hobby
        {
            Text = resource.Name,
            Icon = resource.Icon
        };

        await _hobbyRepository.AddHobbyAsync(hobby);
        _logger.LogInformation("Added skill '{}'", resource.Name);
    }

    public override async Task UpdateResourceAsync(HobbyDto resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        var hobby = await _hobbyRepository.GetHobbyAsync(resource.Id)
            ?? throw new InvalidOperationException($"No hobby with id '{resource.Id}' found");

        if (!resource.IsEqualToModel(hobby))
        {
            hobby.Text = resource.Name;
            hobby.Icon = resource.Icon;

            await _hobbyRepository.UpdateHobbyAsync(hobby);
            _logger.LogInformation("Updated skill '{}'", resource.Id);
        }
    }

    public override async Task DeleteResourceAsync(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Provided id {id} is invalid.");
        }

        await _hobbyRepository.DeleteHobbyAsync(id);
        _logger.LogInformation("Removed hobby '{}'", id);
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
