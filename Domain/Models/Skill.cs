namespace Domain.Models;

public class Skill
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public int Level { get; set; }
}