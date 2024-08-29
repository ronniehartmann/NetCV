using Application.Services.Settings;
using Application.Services.Settings.Impl;
using Domain.Models;
using Domain.Repositories;
using Moq;
using Tests.DummyData;

namespace Tests.Services;

public class SettingsServiceTests
{
    private ISettingsService _settingsService;
    private Mock<ISettingsRepository> _settingsRepositoryMock;

    [SetUp]
    public void Setup()
    {
        _settingsRepositoryMock = new Mock<ISettingsRepository>();
        _settingsService = new SettingsService(_settingsRepositoryMock.Object);
    }

    [Test]
    public async Task GetFavIconFileNameAsync_Exists_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.DEFAULT_SETTINGS);

        // Act
        var favIconFileName = await _settingsService.GetFavIconFileNameAsync();

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        Assert.That(favIconFileName, Is.EqualTo(SettingsData.DEFAULT_SETTINGS.FavIconFileName));
    }

    [Test]
    public async Task GetFavIconFileNameAsync_Null_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.EMPTY_SETTINGS);

        // Act
        var favIconFileName = await _settingsService.GetFavIconFileNameAsync();

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        Assert.That(favIconFileName, Is.EqualTo(null));
    }

    [Test]
    public async Task GetPortraitFileNameAsync_Exists_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.DEFAULT_SETTINGS);

        // Act
        var portraitFileName = await _settingsService.GetPortraitFileNameAsync();

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        Assert.That(portraitFileName, Is.EqualTo(SettingsData.DEFAULT_SETTINGS.PortraitImageFileName));
    }

    [Test]
    public async Task GetPortraitFileNameAsync_Null_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.EMPTY_SETTINGS);

        // Act
        var portraitFileName = await _settingsService.GetPortraitFileNameAsync();

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        Assert.That(portraitFileName, Is.EqualTo(null));
    }

    [Test]
    public async Task GetBackgroundFileNameAsync_Exists_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.DEFAULT_SETTINGS);

        // Act
        var backgroundFileName = await _settingsService.GetBackgroundFileNameAsync();

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        Assert.That(backgroundFileName, Is.EqualTo(SettingsData.DEFAULT_SETTINGS.BackgroundImageFileName));
    }

    [Test]
    public async Task GetBackgroundFileNameAsync_Null_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.EMPTY_SETTINGS);

        // Act
        var backgroundFileName = await _settingsService.GetBackgroundFileNameAsync();

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once());
        Assert.That(backgroundFileName, Is.EqualTo(null));
    }

    [Test]
    public async Task GetOpenToWorkAsync_True_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.DEFAULT_SETTINGS);

        // Act
        var openToWork = await _settingsService.GetOpenToWorkAsync();

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        Assert.That(openToWork, Is.EqualTo(SettingsData.DEFAULT_SETTINGS.OpenToWork));
    }

    [Test]
    public async Task GetOpenToWorkAsync_False_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.EMPTY_SETTINGS);

        // Act
        var openToWork = await _settingsService.GetOpenToWorkAsync();

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        Assert.That(openToWork, Is.EqualTo(false));
    }

    [Test]
    public async Task GetShowFooterAsync_True_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.DEFAULT_SETTINGS);

        // Act
        var showFooter = await _settingsService.GetShowFooterAsync();

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        Assert.That(showFooter, Is.EqualTo(SettingsData.DEFAULT_SETTINGS.OpenToWork));
    }

    [Test]
    public async Task GetShowFooterAsync_False_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.EMPTY_SETTINGS);

        // Act
        var showFooter = await _settingsService.GetShowFooterAsync();

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        Assert.That(showFooter, Is.EqualTo(false));
    }

    [Test]
    public async Task GetShowVersionAsync_True_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.DEFAULT_SETTINGS);

        // Act
        var showVersion = await _settingsService.GetShowVersionAsync();

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        Assert.That(showVersion, Is.EqualTo(SettingsData.DEFAULT_SETTINGS.OpenToWork));
    }

    [Test]
    public async Task GetShowVersionAsync_False_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.EMPTY_SETTINGS);

        // Act
        var showVersion = await _settingsService.GetShowVersionAsync();

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        Assert.That(showVersion, Is.EqualTo(false));
    }

    [Test]
    public async Task GetShowPoweredByNetCvAsync_True_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.DEFAULT_SETTINGS);

        // Act
        var showPoweredByNetCv = await _settingsService.GetShowPoweredByNetCvAsync();

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        Assert.That(showPoweredByNetCv, Is.EqualTo(SettingsData.DEFAULT_SETTINGS.OpenToWork));
    }

    [Test]
    public async Task GetShowPoweredByNetCvAsync_False_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.EMPTY_SETTINGS);

        // Act
        var showPoweredByNetCv = await _settingsService.GetShowPoweredByNetCvAsync();

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        Assert.That(showPoweredByNetCv, Is.EqualTo(false));
    }

    [Test]
    public async Task UpdateFavIconFileNameAsync_String_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.EMPTY_SETTINGS);
        var fileName = "favicon.ico";

        // Act
        await _settingsService.UpdateFavIconFileNameAsync(fileName);

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        _settingsRepositoryMock.Verify(x => x.SetSettingsAsync(It.IsAny<Settings>()), Times.Once);
    }

    [Test]
    public void UpdateFavIconFileNameAsync_Whitespace_ArgumentException()
    {
        // Arrange
        var fileName = " ";

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(async ()
            => await _settingsService.UpdateFavIconFileNameAsync(fileName));

        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Never);
        _settingsRepositoryMock.Verify(x => x.SetSettingsAsync(It.IsAny<Settings>()), Times.Never);
    }

    [Test]
    public async Task UpdatePortraitFileNameAsync_String_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.EMPTY_SETTINGS);
        var fileName = "portrait.png";

        // Act
        await _settingsService.UpdatePortraitFileNameAsync(fileName);

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        _settingsRepositoryMock.Verify(x => x.SetSettingsAsync(It.IsAny<Settings>()), Times.Once);
    }

    [Test]
    public void UpdatePortraitFileNameAsync_Whitespace_ArgumentException()
    {
        // Arrange
        var fileName = " ";

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(async ()
            => await _settingsService.UpdatePortraitFileNameAsync(fileName));

        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Never);
        _settingsRepositoryMock.Verify(x => x.SetSettingsAsync(It.IsAny<Settings>()), Times.Never);
    }

    [Test]
    public async Task UpdateBackgroundFileNameAsync_String_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.EMPTY_SETTINGS);
        var fileName = "background.png";

        // Act
        await _settingsService.UpdateBackgroundFileNameAsync(fileName);

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        _settingsRepositoryMock.Verify(x => x.SetSettingsAsync(It.IsAny<Settings>()), Times.Once);
    }

    [Test]
    public void UpdateBackgroundFileNameAsync_Whitespace_ArgumentException()
    {
        // Arrange
        var fileName = " ";

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(async ()
            => await _settingsService.UpdateBackgroundFileNameAsync(fileName));

        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Never);
        _settingsRepositoryMock.Verify(x => x.SetSettingsAsync(It.IsAny<Settings>()), Times.Never);
    }

    [Test]
    public async Task UpdateOpenToWorkAsync_True_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.EMPTY_SETTINGS);
        var openToWork = true;

        // Act
        await _settingsService.UpdateOpenToWorkAsync(openToWork);

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        _settingsRepositoryMock.Verify(x => x.SetSettingsAsync(It.IsAny<Settings>()), Times.Once);
    }

    [Test]
    public async Task UpdateShowFooterAsync_True_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.EMPTY_SETTINGS);
        var showFooter = true;

        // Act
        await _settingsService.UpdateShowFooterAsync(showFooter);

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        _settingsRepositoryMock.Verify(x => x.SetSettingsAsync(It.IsAny<Settings>()), Times.Once);
    }

    [Test]
    public async Task UpdateShowVersionAsync_True_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.EMPTY_SETTINGS);
        var showVersion = true;

        // Act
        await _settingsService.UpdateShowVersionAsync(showVersion);

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        _settingsRepositoryMock.Verify(x => x.SetSettingsAsync(It.IsAny<Settings>()), Times.Once);
    }

    [Test]
    public async Task UpdateShowPoweredByNetCvAsync_True_Success()
    {
        // Arrange
        _settingsRepositoryMock.Setup(x => x.GetSettingsAsync()).ReturnsAsync(SettingsData.EMPTY_SETTINGS);
        var showPoweredByNetCv = true;

        // Act
        await _settingsService.UpdateShowPoweredByNetCvAsync(showPoweredByNetCv);

        // Assert
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once);
        _settingsRepositoryMock.Verify(x => x.SetSettingsAsync(It.IsAny<Settings>()), Times.Once);
    }
}
