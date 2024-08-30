using Application.Dtos;
using Application.Services.Resources;
using Application.Services.Resources.Impl;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Tests.DummyData;

namespace Tests.Services.Resources;

public class SkillServiceTests
{
    private ResourceService<SkillDto> _skillService;
    private Mock<ISkillRepository> _skillRepositoryMock;
    private Mock<ILogger<SkillService>> _loggerMock;

    [SetUp]
    public void Setup()
    {
        _skillRepositoryMock = new Mock<ISkillRepository>();
        _loggerMock = new Mock<ILogger<SkillService>>();
        _skillService = new SkillService(_skillRepositoryMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetResourcesAsync_HasResources_Success()
    {
        // Arrange
        _skillRepositoryMock.Setup(x => x.GetAllSkillsAsync()).ReturnsAsync(SkillData.DEFAULT_SKILLS);

        // Act
        var resources = await _skillService.GetResourcesAsync();

        // Assert
        Assert.That(resources, Has.Exactly(2).Items);
        Assert.That(resources, Has.One.Matches<SkillDto>(x => string.Equals(x.Name, "C#")));
    }

    [Test]
    public async Task AddResourceAsync_Resource_Success()
    {
        // Arrange
        _skillRepositoryMock.Setup(x => x.GetAllSkillsAsync()).ReturnsAsync([]);

        // Act
        await _skillService.AddResourceAsync(SkillData.DEFAULT_SKILL_DTO);

        // Assert
        _skillRepositoryMock.Verify(x => x.AddSkillAsync(It.IsAny<Skill>()), Times.Once);
    }

    [Test]
    public void AddResourceAsync_Null_ArgumentNullException()
    {
        // Arrange
        _skillRepositoryMock.Setup(x => x.GetAllSkillsAsync()).ReturnsAsync([]);

        // Act & Assert
        Assert.That(async () => await _skillService.AddResourceAsync(null!), Throws.TypeOf<ArgumentNullException>());
        _skillRepositoryMock.Verify(x => x.AddSkillAsync(It.IsAny<Skill>()), Times.Never);
    }

    [Test]
    public async Task UpdateResourceAsync_Resource_Success()
    {
        // Arrange
        _skillRepositoryMock.Setup(x => x.GetSkillAsync(It.IsAny<long>())).ReturnsAsync(SkillData.DEFAULT_SKILLS.First());
        var resourceToUpdate = SkillData.DEFAULT_SKILL_DTO;
        resourceToUpdate.Name = ".NET";

        // Act
        await _skillService.UpdateResourceAsync(resourceToUpdate);

        // Assert
        _skillRepositoryMock.Verify(x => x.UpdateSkillAsync(It.IsAny<Skill>()), Times.Once);
    }

    [Test]
    public void UpdateResourceAsync_Null_ArgumentNullException()
    {
        // Act & Assert
        Assert.That(async () => await _skillService.UpdateResourceAsync(null!), Throws.TypeOf<ArgumentNullException>());
        _skillRepositoryMock.Verify(x => x.UpdateSkillAsync(It.IsAny<Skill>()), Times.Never);
    }

    [Test]
    public void UpdateResourceAsync_MissingEducation_InvalidOperationException()
    {
        // Arrange
        _skillRepositoryMock.Setup(x => x.GetSkillAsync(It.IsAny<long>())).ReturnsAsync((Skill)null!);

        // Act & Assert
        Assert.That(async () => await _skillService.UpdateResourceAsync(SkillData.DEFAULT_SKILL_DTO), Throws.TypeOf<InvalidOperationException>());
        _skillRepositoryMock.Verify(x => x.UpdateSkillAsync(It.IsAny<Skill>()), Times.Never);
    }

    [Test]
    public async Task DeleteResourceAsync_ValidId_Success()
    {
        // Act
        await _skillService.DeleteResourceAsync(1);

        // Assert
        _skillRepositoryMock.Verify(x => x.DeleteSkillAsync(It.IsAny<long>()), Times.Once);
    }

    [Test]
    public void DeleteResourceAsync_NegativeId_ArgumentException()
    {
        // Act & Assert
        Assert.That(async () => await _skillService.DeleteResourceAsync(-1), Throws.TypeOf<ArgumentException>());
        _skillRepositoryMock.Verify(x => x.DeleteSkillAsync(It.IsAny<long>()), Times.Never);
    }
}
