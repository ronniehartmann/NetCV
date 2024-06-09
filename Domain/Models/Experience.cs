namespace Domain.Models;

public class Experience
{
    public long Id { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public required string Company { get; set; }

    public string? CompanyLink { get; set; }

    public string? Text { get; set; }
}