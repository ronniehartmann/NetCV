using Application.Dtos;
using Application.Services.Resources;
using Application.Services.Resources.Impl;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Tests.DummyData;

namespace Tests.Services.Resources;

public class HobbyServiceTests
{
    private ResourceService<HobbyDto> _hobbyService;
    private Mock<IHobbyRepository> _hobbyRepositoryMock;
    private Mock<ILogger<HobbyService>> _loggerMock;

    [SetUp]
    public void Setup()
    {
        _hobbyRepositoryMock = new Mock<IHobbyRepository>();
        _loggerMock = new Mock<ILogger<HobbyService>>();
        _hobbyService = new HobbyService(_hobbyRepositoryMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetResourcesAsync_HasResources_Success()
    {
        // Arrange
        _hobbyRepositoryMock.Setup(x => x.GetAllHobbiesAsync()).ReturnsAsync(HobbyData.DEFAULT_HOBBIES);

        // Act
        var resources = await _hobbyService.GetResourcesAsync();

        // Assert
        Assert.That(resources, Has.Exactly(2).Items);
        Assert.That(resources, Has.One.Matches<HobbyDto>(x => string.Equals(x.Name, "Skateboarding")));
    }

    [Test]
    public async Task AddResourceAsync_Resource_Success()
    {
        // Arrange
        _hobbyRepositoryMock.Setup(x => x.GetAllHobbiesAsync()).ReturnsAsync([]);

        // Act
        await _hobbyService.AddResourceAsync(HobbyData.DEFAULT_HOBBY_DTO);

        // Assert
        _hobbyRepositoryMock.Verify(x => x.AddHobbyAsync(It.IsAny<Hobby>()), Times.Once);
    }

    [Test]
    public void AddResourceAsync_Null_ArgumentNullException()
    {
        // Arrange
        _hobbyRepositoryMock.Setup(x => x.GetAllHobbiesAsync()).ReturnsAsync([]);

        // Act & Assert
        Assert.That(async () => await _hobbyService.AddResourceAsync(null!), Throws.TypeOf<ArgumentNullException>());
        _hobbyRepositoryMock.Verify(x => x.AddHobbyAsync(It.IsAny<Hobby>()), Times.Never);
    }

    [Test]
    public async Task UpdateResourceAsync_Resource_Success()
    {
        // Arrange
        _hobbyRepositoryMock.Setup(x => x.GetHobbyAsync(It.IsAny<long>())).ReturnsAsync(HobbyData.DEFAULT_HOBBIES.First());
        var resourceToUpdate = HobbyData.DEFAULT_HOBBY_DTO;
        resourceToUpdate.Name = "Other Hobby";

        // Act
        await _hobbyService.UpdateResourceAsync(resourceToUpdate);

        // Assert
        _hobbyRepositoryMock.Verify(x => x.UpdateHobbyAsync(It.IsAny<Hobby>()), Times.Once);
    }

    [Test]
    public void UpdateResourceAsync_Null_ArgumentNullException()
    {
        // Act & Assert
        Assert.That(async () => await _hobbyService.UpdateResourceAsync(null!), Throws.TypeOf<ArgumentNullException>());
        _hobbyRepositoryMock.Verify(x => x.UpdateHobbyAsync(It.IsAny<Hobby>()), Times.Never);
    }

    [Test]
    public void UpdateResourceAsync_MissingEducation_InvalidOperationException()
    {
        // Arrange
        _hobbyRepositoryMock.Setup(x => x.GetHobbyAsync(It.IsAny<long>())).ReturnsAsync((Hobby)null!);

        // Act & Assert
        Assert.That(async () => await _hobbyService.UpdateResourceAsync(HobbyData.DEFAULT_HOBBY_DTO), Throws.TypeOf<InvalidOperationException>());
        _hobbyRepositoryMock.Verify(x => x.UpdateHobbyAsync(It.IsAny<Hobby>()), Times.Never);
    }

    [Test]
    public async Task DeleteResourceAsync_ValidId_Success()
    {
        // Act
        await _hobbyService.DeleteResourceAsync(1);

        // Assert
        _hobbyRepositoryMock.Verify(x => x.DeleteHobbyAsync(It.IsAny<long>()), Times.Once);
    }

    [Test]
    public void DeleteResourceAsync_NegativeId_ArgumentException()
    {
        // Act & Assert
        Assert.That(async () => await _hobbyService.DeleteResourceAsync(-1), Throws.TypeOf<ArgumentException>());
        _hobbyRepositoryMock.Verify(x => x.DeleteHobbyAsync(It.IsAny<long>()), Times.Never);
    }
}
