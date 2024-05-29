namespace Application.Dtos;

public class ReferenceDto
{
    public long Id { get; set; }

    public required string Title { get; set; }

    public string? Employment { get; set; }

    public required string Text { get; set; }
}