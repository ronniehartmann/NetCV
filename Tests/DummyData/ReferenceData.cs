using Application.Dtos;
using Domain.Models;

namespace Tests.DummyData;

public static class ReferenceData
{
    public static readonly IEnumerable<Reference> DEFAULT_REFERENCES =
    [
        new()
        {
            Id = 1,
            Title = "Max Mustermann",
            Employment = "Teacher",
            Text = "Hello World!"
        },
        new()
        {
            Id = 2,
            Title = "John Doe",
            Employment = "Software Engineer",
            Text = "Cool"
        }
    ];

    public static readonly ReferenceDto DEFAULT_REFERENCE_DTO = new()
    {
        Id = 1,
        Title = "Max Mustermann",
        Employment = "Teacher",
        Text = "Hello World!"
    };
}
