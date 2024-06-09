namespace Application.Dtos;

public class ExperienceDto
{
    public long Id { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string Company { get; set; } = string.Empty;

    public string? CompanyLink { get; set; }

    public string? Text { get; set; }
}
