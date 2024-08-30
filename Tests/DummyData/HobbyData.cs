using Application.Dtos;
using Domain.Models;

namespace Tests.DummyData;

public static class HobbyData
{
    public static readonly IEnumerable<Hobby> DEFAULT_HOBBIES =
    [
        new()
        {
            Id = 1,
            Text = "Skateboarding",
            Icon = "skateboarding"
        },
        new()
        {
            Id = 2,
            Text = "Football",
            Icon = "sports_soccer"
        }
    ];

    public static readonly HobbyDto DEFAULT_HOBBY_DTO = new()
    {
        Id = 1,
        Name = "Skateboarding",
        Icon = "skateboarding"
    };
}
