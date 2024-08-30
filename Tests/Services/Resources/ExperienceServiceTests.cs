using Application.Dtos;
using Application.Services.Resources;
using Application.Services.Resources.Impl;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Tests.DummyData;

namespace Tests.Services.Resources;

public class ExperienceServiceTests
{
    private ResourceService<ExperienceDto> _experienceService;
    private Mock<IExperienceRepository> _experienceRepositoryMock;
    private Mock<ILogger<ExperienceService>> _loggerMock;

    [SetUp]
    public void Setup()
    {
        _experienceRepositoryMock = new Mock<IExperienceRepository>();
        _loggerMock = new Mock<ILogger<ExperienceService>>();
        _experienceService = new ExperienceService(_experienceRepositoryMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetResourcesAsync_HasResources_Success()
    {
        // Arrange
        _experienceRepositoryMock.Setup(x => x.GetAllExperiencesAsync()).ReturnsAsync(ExperienceData.DEFAULT_EXPERIENCES);

        // Act
        var resources = await _experienceService.GetResourcesAsync();

        // Assert
        Assert.That(resources, Has.Exactly(2).Items);
        Assert.That(resources, Has.One.Matches<ExperienceDto>(x => string.Equals(x.Company, "NetCV")));
    }

    [Test]
    public async Task AddResourceAsync_Resource_Success()
    {
        // Arrange
        _experienceRepositoryMock.Setup(x => x.GetAllExperiencesAsync()).ReturnsAsync([]);

        // Act
        await _experienceService.AddResourceAsync(ExperienceData.DEFAULT_EXPERIENCE_DTO);

        // Assert
        _experienceRepositoryMock.Verify(x => x.AddExperienceAsync(It.IsAny<Experience>()), Times.Once);
    }

    [Test]
    public void AddResourceAsync_Null_ArgumentNullException()
    {
        // Arrange
        _experienceRepositoryMock.Setup(x => x.GetAllExperiencesAsync()).ReturnsAsync([]);

        // Act & Assert
        Assert.That(async () => await _experienceService.AddResourceAsync(null!), Throws.TypeOf<ArgumentNullException>());
        _experienceRepositoryMock.Verify(x => x.AddExperienceAsync(It.IsAny<Experience>()), Times.Never);
    }

    [Test]
    public async Task UpdateResourceAsync_Resource_Success()
    {
        // Arrange
        _experienceRepositoryMock.Setup(x => x.GetExperienceAsync(It.IsAny<long>())).ReturnsAsync(ExperienceData.DEFAULT_EXPERIENCES.First());
        var resourceToUpdate = ExperienceData.DEFAULT_EXPERIENCE_DTO;
        resourceToUpdate.Company = "Other Company";

        // Act
        await _experienceService.UpdateResourceAsync(resourceToUpdate);

        // Assert
        _experienceRepositoryMock.Verify(x => x.UpdateExperienceAsync(It.IsAny<Experience>()), Times.Once);
    }

    [Test]
    public void UpdateResourceAsync_Null_ArgumentNullException()
    {
        // Act & Assert
        Assert.That(async () => await _experienceService.UpdateResourceAsync(null!), Throws.TypeOf<ArgumentNullException>());
        _experienceRepositoryMock.Verify(x => x.UpdateExperienceAsync(It.IsAny<Experience>()), Times.Never);
    }

    [Test]
    public void UpdateResourceAsync_MissingEducation_InvalidOperationException()
    {
        // Arrange
        _experienceRepositoryMock.Setup(x => x.GetExperienceAsync(It.IsAny<long>())).ReturnsAsync((Experience)null!);

        // Act & Assert
        Assert.That(async () => await _experienceService.UpdateResourceAsync(ExperienceData.DEFAULT_EXPERIENCE_DTO), Throws.TypeOf<InvalidOperationException>());
        _experienceRepositoryMock.Verify(x => x.UpdateExperienceAsync(It.IsAny<Experience>()), Times.Never);
    }

    [Test]
    public async Task DeleteResourceAsync_ValidId_Success()
    {
        // Act
        await _experienceService.DeleteResourceAsync(1);

        // Assert
        _experienceRepositoryMock.Verify(x => x.DeleteExperienceAsync(It.IsAny<long>()), Times.Once);
    }

    [Test]
    public void DeleteResourceAsync_NegativeId_ArgumentException()
    {
        // Act & Assert
        Assert.That(async () => await _experienceService.DeleteResourceAsync(-1), Throws.TypeOf<ArgumentException>());
        _experienceRepositoryMock.Verify(x => x.DeleteExperienceAsync(It.IsAny<long>()), Times.Never);
    }
}
