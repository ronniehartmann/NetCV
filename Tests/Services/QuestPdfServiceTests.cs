using Application.Dtos;
using Application.Services.Contents;
using Application.Services.Pdf.Impl;
using Application.Services.Resources;
using Microsoft.Extensions.Logging;
using Moq;
using Tests.DummyData;

namespace Tests.Services;

public class QuestPdfServiceTests
{
    private QuestPdfService _pdfService;
    private Mock<ILogger<QuestPdfService>> _loggerMock;
    private Mock<IContentService> _contentServiceMock;
    private Mock<ResourceService<HobbyDto>> _hobbyServiceMock;
    private Mock<ResourceService<SkillDto>> _skillServiceMock;
    private Mock<ResourceService<ExperienceDto>> _experienceServiceMock;
    private Mock<ResourceService<EducationDto>> _educationServiceMock;
    private Mock<ResourceService<ReferenceDto>> _referenceServiceMock;

    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<QuestPdfService>>();
        _contentServiceMock = new Mock<IContentService>();
        _hobbyServiceMock = new Mock<ResourceService<HobbyDto>>();
        _skillServiceMock = new Mock<ResourceService<SkillDto>>();
        _experienceServiceMock = new Mock<ResourceService<ExperienceDto>>();
        _educationServiceMock = new Mock<ResourceService<EducationDto>>();
        _referenceServiceMock = new Mock<ResourceService<ReferenceDto>>();

        _pdfService = new QuestPdfService(
            _loggerMock.Object,
            _contentServiceMock.Object,
            _hobbyServiceMock.Object,
            _skillServiceMock.Object,
            _experienceServiceMock.Object,
            _educationServiceMock.Object,
            _referenceServiceMock.Object);
    }

    [Test]
    public async Task GeneratePdfAsync_Success()
    {
        // Arrange
        _contentServiceMock.Setup(x => x.GetValueAsync(It.IsAny<string>())).ReturnsAsync("25/10/2024");
        _hobbyServiceMock.Setup(x => x.GetResourcesAsync()).ReturnsAsync([HobbyData.DEFAULT_HOBBY_DTO]);
        _skillServiceMock.Setup(x => x.GetResourcesAsync()).ReturnsAsync([SkillData.DEFAULT_SKILL_DTO]);
        _experienceServiceMock.Setup(x => x.GetResourcesAsync()).ReturnsAsync([ExperienceData.DEFAULT_EXPERIENCE_DTO]);

        // Act
        var bytes = await _pdfService.GeneratePdfAsync();

        // Assert
        Assert.That(bytes, Is.Not.Null);
    }
}
