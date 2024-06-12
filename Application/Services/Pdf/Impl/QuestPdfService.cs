using Application.Dtos;
using Application.Services.Contents;
using Application.Services.Pdf.Models;
using Application.Services.Pdf.Templates;
using Application.Services.Resources;
using Microsoft.Extensions.Logging;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace Application.Services.Pdf.Impl;

public class QuestPdfService(
    ILogger<QuestPdfService> logger,
    IContentService contentService,
    ResourceService<HobbyDto> hobbyService,
    ResourceService<SkillDto> skillService,
    ResourceService<ExperienceDto> experienceService,
    ResourceService<EducationDto> educationService,
    ResourceService<ReferenceDto> referenceService) : IPdfService
{
    private readonly ILogger<QuestPdfService> _logger = logger;
    private readonly IContentService _contentService = contentService;
    private readonly ResourceService<HobbyDto> _hobbyService = hobbyService;
    private readonly ResourceService<SkillDto> _skillService = skillService;
    private readonly ResourceService<ExperienceDto> _experienceService = experienceService;
    private readonly ResourceService<EducationDto> _educationService = educationService;
    private readonly ResourceService<ReferenceDto> _referenceService = referenceService;

    public async Task<byte[]> GeneratePdfAsync()
    {
        var pdfData = await GetPdfDataAsync();

        QuestPDF.Settings.License = LicenseType.Community;
        var document = new CvDocument(pdfData);
        var bytes = document.GeneratePdf();

        var fileSizeKb = (double)bytes.Length / (1024);
        _logger.LogInformation("Generated PDF (size: {} KB)", $"{fileSizeKb:N2}");

        return bytes;
    }

    private async Task<PdfData> GetPdfDataAsync()
    {
        return new PdfData
        {
            FullName = await _contentService.GetValueAsync("PROFILE_FULLNAME"),
            Employment = await _contentService.GetValueAsync("PROFILE_EMPLOYMENT"),
            AboutText = await _contentService.GetValueAsync("ABOUT_TEXT"),
            Bio = await GetBioDataAsync(),
            Hobbies = (await _hobbyService.GetResourcesAsync()).Select(h => h.Name),
            Skills = (await _skillService.GetResourcesAsync()).Select(s => s.Name),
            Experiences = await GetExperienceDataAsync(),
            Educations = await GetEducationDataAsync(),
            References = await GetReferenceDataAsync()
        };
    }

    private async Task<IEnumerable<BioDataItem>> GetBioDataAsync()
    {
        var birthDate = await _contentService.GetValueAsync("ABOUT_BIRTHDATE");
        var age = (DateTime.Now - DateTime.ParseExact(birthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)).Days / 365;

        return
        [
            new()
            {
                Title = "Age",
                Text = age.ToString(),
                IconPath = "wwwroot/icons/calendar-user-svgrepo-com.svg"
            },
            new()
            {
                Title = "Email",
                Text = await _contentService.GetValueAsync("ABOUT_EMAIL"),
                IconPath = "wwwroot/icons/email-svgrepo-com.svg"
            },
            new()
            {
                Title = "Phone",
                Text = await _contentService.GetValueAsync("ABOUT_PHONE"),
                IconPath = "wwwroot/icons/phone-svgrepo-com.svg"
            },
            new()
            {
                Title = "GitHub",
                Text = await _contentService.GetValueAsync("ABOUT_GITHUB"),
                IconPath = "wwwroot/icons/github-142-svgrepo-com.svg"
            },
            new()
            {
                Title = "Address",
                Text = await _contentService.GetValueAsync("ABOUT_RESIDENCE"),
                IconPath = "wwwroot/icons/address-location-map-svgrepo-com.svg"
            }
        ];
    }

    private async Task<IEnumerable<ExperienceDataItem>> GetExperienceDataAsync()
    {
        var experiences = await _experienceService.GetResourcesAsync();
        return experiences.Select(experience => new ExperienceDataItem 
        {
            Duration = GetDurationText(experience.StartDate, experience.EndDate),
            Company = experience.Company,
            CompanyLink = experience.CompanyLink,
            Text = experience.Text
        });
    }

    private async Task<IEnumerable<EducationDataItem>> GetEducationDataAsync()
    {
        var educations = await _educationService.GetResourcesAsync();
        return educations.Select(education => new EducationDataItem
        {
            Duration = GetDurationText(education.StartDate, education.EndDate),
            School = education.School,
            Title = education.Title
        });
    }

    private async Task<IEnumerable<ReferenceDataItem>> GetReferenceDataAsync()
    {
        var references = await _referenceService.GetResourcesAsync();
        return references.Select(reference => new ReferenceDataItem
        {
            Title = reference.Title,
            Employment = reference.Employment,
            Text = reference.Text
        });
    }

    private static string GetDurationText(DateOnly dateFrom, DateOnly? dateTo)
    {
        var duration = dateFrom.ToString("MMMM yyyy", CultureInfo.InvariantCulture) + " - ";
        if (dateTo.HasValue)
        {
            duration += dateTo.Value.ToString("MMMM yyyy", CultureInfo.InvariantCulture);
        }
        else
        {
            duration += "Today";
        }
        return duration;
    }
}
