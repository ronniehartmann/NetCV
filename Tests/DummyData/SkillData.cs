using Application.Dtos;
using Domain.Models;

namespace Tests.DummyData;

public static class SkillData
{
    public static readonly IEnumerable<Skill> DEFAULT_SKILLS =
    [
        new()
        {
            Id = 1,
            Name = "C#",
            Level = 90
        },
        new()
        {
            Id = 2,
            Name = "Java",
            Level = 10
        }
    ];

    public static readonly SkillDto DEFAULT_SKILL_DTO = new()
    {
        Id = 1,
        Name = "C#",
        Level = 90
    };
}
