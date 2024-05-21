namespace Domain.Models;

public class Hobby
{
    public long Id { get; set; }

    public required string Text { get; set; }

    public string? Icon { get; set; }
}