namespace Application.Services.Pdf.Models;

public class PdfData
{
    public string FullName { get; set; } = string.Empty;

    public string Employment { get; set; } = string.Empty;

    public string AboutText { get; set; } = string.Empty;

    public IEnumerable<BioDataItem> Bio { get; set; } = [];

    public IEnumerable<string> Hobbies { get; set; } = [];

    public IEnumerable<string> Skills { get; set; } = [];

    public IEnumerable<ExperienceDataItem> Experiences { get; set; } = [];

    public IEnumerable<EducationDataItem> Educations { get; set; } = [];

    public IEnumerable<ReferenceDataItem> References { get; set; } = [];
}