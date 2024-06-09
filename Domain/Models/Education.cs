namespace Domain.Models;

public class Education
{
    public long Id { get; set; }

    public required string School { get; set; }

    public required string Title { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }
}
