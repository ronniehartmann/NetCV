using Application.Services.Settings;
using Application.Services.Settings.Impl;
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
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once());
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
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once());
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
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once());
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
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once());
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
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once());
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
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once());
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
        _settingsRepositoryMock.Verify(x => x.GetSettingsAsync(), Times.Once());
        Assert.That(openToWork, Is.EqualTo(false));
    }
}
