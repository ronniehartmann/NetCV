namespace Application.Dtos;

public class ExperienceDto
{
    public long Id { get; set; }

    public string Company { get; set; } = string.Empty;

    public string? Text { get; set; }

    public string Duration { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}
