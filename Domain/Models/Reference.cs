namespace Domain.Models;

public class Reference
{
    public long Id { get; set; }

    public required string Title { get; set; }

    public string? Employment { get; set; }

    public required string Text { get; set; }
}
