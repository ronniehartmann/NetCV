using Application.Dtos;
using Application.Services.Resources;
using Application.Services.Resources.Impl;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Tests.DummyData;

namespace Tests.Services.Resources;

public class EducationServiceTests
{
    private ResourceService<EducationDto> _educationService;
    private Mock<IEducationRepository> _educationRepositoryMock;
    private Mock<ILogger<EducationService>> _loggerMock;

    [SetUp]
    public void Setup()
    {
        _educationRepositoryMock = new Mock<IEducationRepository>();
        _loggerMock = new Mock<ILogger<EducationService>>();
        _educationService = new EducationService(_educationRepositoryMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetResourcesAsync_HasResources_Success()
    {
        // Arrange
        _educationRepositoryMock.Setup(x => x.GetAllEducationsAsync()).ReturnsAsync(EducationData.DEFAULT_EDUCATIONS);

        // Act
        var resources = await _educationService.GetResourcesAsync();

        // Assert
        Assert.That(resources, Has.Exactly(2).Items);
        Assert.That(resources, Has.One.Matches<EducationDto>(x => string.Equals(x.School, "NetCV School")));
    }

    [Test]
    public async Task AddResourceAsync_Resource_Success()
    {
        // Arrange
        _educationRepositoryMock.Setup(x => x.GetAllEducationsAsync()).ReturnsAsync([]);

        // Act
        await _educationService.AddResourceAsync(EducationData.DEFAULT_EDUCATION_DTO);

        // Assert
        _educationRepositoryMock.Verify(x => x.AddEducationAsync(It.IsAny<Education>()), Times.Once);
    }

    [Test]
    public void AddResourceAsync_Null_ArgumentNullException()
    {
        // Arrange
        _educationRepositoryMock.Setup(x => x.GetAllEducationsAsync()).ReturnsAsync([]);

        // Act & Assert
        Assert.That(async () => await _educationService.AddResourceAsync(null!), Throws.TypeOf<ArgumentNullException>());
        _educationRepositoryMock.Verify(x => x.AddEducationAsync(It.IsAny<Education>()), Times.Never);
    }

    [Test]
    public async Task UpdateResourceAsync_Resource_Success()
    {
        // Arrange
        _educationRepositoryMock.Setup(x => x.GetEducationAsync(It.IsAny<long>())).ReturnsAsync(EducationData.DEFAULT_EDUCATIONS.First());
        var resourceToUpdate = EducationData.DEFAULT_EDUCATION_DTO;
        resourceToUpdate.School = "Other School";

        // Act
        await _educationService.UpdateResourceAsync(resourceToUpdate);

        // Assert
        _educationRepositoryMock.Verify(x => x.UpdateEducationAsync(It.IsAny<Education>()), Times.Once);
    }

    [Test]
    public void UpdateResourceAsync_Null_ArgumentNullException()
    {
        // Act & Assert
        Assert.That(async () => await _educationService.UpdateResourceAsync(null!), Throws.TypeOf<ArgumentNullException>());
        _educationRepositoryMock.Verify(x => x.UpdateEducationAsync(It.IsAny<Education>()), Times.Never);
    }

    [Test]
    public void UpdateResourceAsync_MissingEducation_InvalidOperationException()
    {
        // Arrange
        _educationRepositoryMock.Setup(x => x.GetEducationAsync(It.IsAny<long>())).ReturnsAsync((Education)null!);

        // Act & Assert
        Assert.That(async () => await _educationService.UpdateResourceAsync(EducationData.DEFAULT_EDUCATION_DTO), Throws.TypeOf<InvalidOperationException>());
        _educationRepositoryMock.Verify(x => x.UpdateEducationAsync(It.IsAny<Education>()), Times.Never);
    }

    [Test]
    public async Task DeleteResourceAsync_ValidId_Success()
    {
        // Act
        await _educationService.DeleteResourceAsync(1);

        // Assert
        _educationRepositoryMock.Verify(x => x.DeleteEducationAsync(It.IsAny<long>()), Times.Once);
    }

    [Test]
    public void DeleteResourceAsync_NegativeId_ArgumentException()
    {
        // Act & Assert
        Assert.That(async () => await _educationService.DeleteResourceAsync(-1), Throws.TypeOf<ArgumentException>());
        _educationRepositoryMock.Verify(x => x.DeleteEducationAsync(It.IsAny<long>()), Times.Never);
    }
}
