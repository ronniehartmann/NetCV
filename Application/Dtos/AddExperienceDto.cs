namespace Application.Dtos;

public class AddExperienceDto
{
    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public required string Company { get; set; }

    public string? Text { get; set; }
}
