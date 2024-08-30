using Application.Dtos;
using Application.Services.Resources;
using Application.Services.Resources.Impl;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Tests.DummyData;

namespace Tests.Services.Resources;

public class ReferenceServiceTests
{
    private ResourceService<ReferenceDto> _referenceService;
    private Mock<IReferenceRepository> _referenceRepositoryMock;
    private Mock<ILogger<ReferenceService>> _loggerMock;

    [SetUp]
    public void Setup()
    {
        _referenceRepositoryMock = new Mock<IReferenceRepository>();
        _loggerMock = new Mock<ILogger<ReferenceService>>();
        _referenceService = new ReferenceService(_referenceRepositoryMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetResourcesAsync_HasResources_Success()
    {
        // Arrange
        _referenceRepositoryMock.Setup(x => x.GetAllReferencesAsync()).ReturnsAsync(ReferenceData.DEFAULT_REFERENCES);

        // Act
        var resources = await _referenceService.GetResourcesAsync();

        // Assert
        Assert.That(resources, Has.Exactly(2).Items);
        Assert.That(resources, Has.One.Matches<ReferenceDto>(x => string.Equals(x.Title, "Max Mustermann")));
    }

    [Test]
    public async Task AddResourceAsync_Resource_Success()
    {
        // Arrange
        _referenceRepositoryMock.Setup(x => x.GetAllReferencesAsync()).ReturnsAsync([]);

        // Act
        await _referenceService.AddResourceAsync(ReferenceData.DEFAULT_REFERENCE_DTO);

        // Assert
        _referenceRepositoryMock.Verify(x => x.AddReferenceAsync(It.IsAny<Reference>()), Times.Once);
    }

    [Test]
    public void AddResourceAsync_Null_ArgumentNullException()
    {
        // Arrange
        _referenceRepositoryMock.Setup(x => x.GetAllReferencesAsync()).ReturnsAsync([]);

        // Act & Assert
        Assert.That(async () => await _referenceService.AddResourceAsync(null!), Throws.TypeOf<ArgumentNullException>());
        _referenceRepositoryMock.Verify(x => x.AddReferenceAsync(It.IsAny<Reference>()), Times.Never);
    }

    [Test]
    public async Task UpdateResourceAsync_Resource_Success()
    {
        // Arrange
        _referenceRepositoryMock.Setup(x => x.GetReferenceAsync(It.IsAny<long>())).ReturnsAsync(ReferenceData.DEFAULT_REFERENCES.First());
        var resourceToUpdate = ReferenceData.DEFAULT_REFERENCE_DTO;
        resourceToUpdate.Title = "Ronnie Hartmann";

        // Act
        await _referenceService.UpdateResourceAsync(resourceToUpdate);

        // Assert
        _referenceRepositoryMock.Verify(x => x.UpdateReferenceAsync(It.IsAny<Reference>()), Times.Once);
    }

    [Test]
    public void UpdateResourceAsync_Null_ArgumentNullException()
    {
        // Act & Assert
        Assert.That(async () => await _referenceService.UpdateResourceAsync(null!), Throws.TypeOf<ArgumentNullException>());
        _referenceRepositoryMock.Verify(x => x.UpdateReferenceAsync(It.IsAny<Reference>()), Times.Never);
    }

    [Test]
    public void UpdateResourceAsync_MissingEducation_InvalidOperationException()
    {
        // Arrange
        _referenceRepositoryMock.Setup(x => x.GetReferenceAsync(It.IsAny<long>())).ReturnsAsync((Reference)null!);

        // Act & Assert
        Assert.That(async () => await _referenceService.UpdateResourceAsync(ReferenceData.DEFAULT_REFERENCE_DTO), Throws.TypeOf<InvalidOperationException>());
        _referenceRepositoryMock.Verify(x => x.UpdateReferenceAsync(It.IsAny<Reference>()), Times.Never);
    }

    [Test]
    public async Task DeleteResourceAsync_ValidId_Success()
    {
        // Act
        await _referenceService.DeleteResourceAsync(1);

        // Assert
        _referenceRepositoryMock.Verify(x => x.DeleteReferenceAsync(It.IsAny<long>()), Times.Once);
    }

    [Test]
    public void DeleteResourceAsync_NegativeId_ArgumentException()
    {
        // Act & Assert
        Assert.That(async () => await _referenceService.DeleteResourceAsync(-1), Throws.TypeOf<ArgumentException>());
        _referenceRepositoryMock.Verify(x => x.DeleteReferenceAsync(It.IsAny<long>()), Times.Never);
    }
}
