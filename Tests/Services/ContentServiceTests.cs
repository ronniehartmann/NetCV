using Application.Services.Contents.Impl;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Tests.DummyData;

namespace Tests.Services;

public class ContentServiceTests
{
    private ContentService _contentService;
    private Mock<IContentRepository> _contentRepositoryMock;
    private Mock<ILogger<ContentService>> _loggerMock;

    [SetUp]
    public void Setup()
    {
        _contentRepositoryMock = new Mock<IContentRepository>();
        _loggerMock = new Mock<ILogger<ContentService>>();
        _contentService = new ContentService(_contentRepositoryMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAllValuesAsync_HasData_Success()
    {
        // Arrange
        _contentRepositoryMock.Setup(x => x.GetAllContentsAsync()).ReturnsAsync(ContentData.DEFAULT_CONTENTS);

        // Act
        var contents = await _contentService.GetAllValuesAsync();

        // Assert
        Assert.That(contents, Has.Exactly(2).Items);
        Assert.That(contents, Has.One.Matches<KeyValuePair<string, string>>(x
            => string.Equals(x.Key, "Name") && string.Equals(x.Value, "NetCV")));
    }

    [Test]
    public async Task GetValueAsync_Exists_Success()
    {
        // Arrange
        _contentRepositoryMock.Setup(x => x.GetContentAsync(It.IsAny<string>())).ReturnsAsync(ContentData.DEFAULT_CONTENTS.First());

        // Act
        var content = await _contentService.GetValueAsync("Name");

        // Assert
        Assert.That(content, Is.Not.Null);
        Assert.That(content, Is.EqualTo("NetCV"));
    }

    [Test]
    public void GetValueAsync_NotFound_InvalidOperationException()
    {
        // Arrange
        _contentRepositoryMock.Setup(x => x.GetContentAsync(It.IsAny<string>())).ReturnsAsync((Content)null!);

        // Act & Assert
        Assert.That(async () => await _contentService.GetValueAsync("Name"), Throws.InvalidOperationException);
    }

    [Test]
    public void GetValueAsync_Null_ArgumentNullException()
    {
        // Act & Assert
        Assert.That(async () => await _contentService.GetValueAsync(null!), Throws.ArgumentNullException);
    }

    [Test]
    public async Task SetValueAsync_NotFound_Success()
    {
        // Arrange
        _contentRepositoryMock.Setup(x => x.GetContentAsync(It.IsAny<string>())).ReturnsAsync((Content)null!);

        // Act
        await _contentService.SetValueAsync("Name", "Ronnie");

        // Assert
        _contentRepositoryMock.Verify(x => x.AddContentAsync(It.IsAny<Content>()), Times.Once);
        _contentRepositoryMock.Verify(x => x.UpdateContentAsync(It.IsAny<Content>()), Times.Never);
    }

    [Test]
    public async Task SetValueAsync_Found_Success()
    {
        // Arrange
        _contentRepositoryMock.Setup(x => x.GetContentAsync(It.IsAny<string>())).ReturnsAsync(ContentData.DEFAULT_CONTENTS.First());

        // Act
        await _contentService.SetValueAsync("Name", "Ronnie");

        // Assert
        _contentRepositoryMock.Verify(x => x.UpdateContentAsync(It.IsAny<Content>()), Times.Once);
        _contentRepositoryMock.Verify(x => x.AddContentAsync(It.IsAny<Content>()), Times.Never);
    }

    [Test]
    public void SetValueAsync_KeyNull_ArgumentNullException()
    {
        // Act & Assert
        Assert.That(async () => await _contentService.SetValueAsync(null!, "Test"), Throws.ArgumentNullException);
    }

    [Test]
    public void SetValueAsync_ValueNull_ArgumentNullException()
    {
        // Act & Assert
        Assert.That(async () => await _contentService.SetValueAsync("Test", null!), Throws.ArgumentNullException);
    }
}
