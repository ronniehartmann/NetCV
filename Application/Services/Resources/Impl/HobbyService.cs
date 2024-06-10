using Application.Dtos;
using Domain.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Services.Resources.Impl;

public class HobbyService(
    IServiceProvider serviceProvider,
    ILogger<HobbyService> logger) : ResourceService<HobbyDto>
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<HobbyService> _logger = logger;

    public override async Task<IEnumerable<HobbyDto>> GetResourcesAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var hobbies = await context.Hobbies.ToListAsync();
        var hobbyDtos = hobbies.Select(ConvertToHobbyDto);
        return hobbyDtos;
    }

    public override async Task AddResourceAsync(HobbyDto resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var hobbyModel = new Hobby
        {
            Text = resource.Name,
            Icon = resource.Icon
        };

        context.Hobbies.Add(hobbyModel);
        await context.SaveChangesAsync();
        _logger.LogInformation("Added skill '{}'", resource.Name);
    }

    public override async Task UpdateResourceAsync(HobbyDto resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CvContext>();

        var existingHobby = await context.Hobbies.FindAsync(resource.Id)
            ?? throw new InvalidOperationException($"No hobby with id '{resource.Id}' found");

        if (!resource.IsEqualToModel(existingHobby))
        {
            existingHobby.Text = resource.Name;
            existingHobby.Icon = resource.Icon;

            await context.SaveChangesAsync();
            _logger.LogInformation("Updated skill '{}'", resource.Id);
        }
    }

    public override async Task DeleteResourceAsync(long id)
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
