namespace Application.Dtos;

public class EducationDto
{
    public long Id { get; set; }

    public string School { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }
}
