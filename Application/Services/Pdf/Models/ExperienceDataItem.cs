namespace Application.Services.Pdf.Models;

public class ExperienceDataItem
{
    public string Duration { get; set; } = string.Empty;

    public string Company { get; set; } = string.Empty;

    public string? CompanyLink { get; set; }

    public string? Text { get; set; }
}