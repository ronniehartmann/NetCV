using Application.Dtos;
using Domain.Models;

namespace Tests.DummyData;

public static class EducationData
{
    public static readonly IEnumerable<Education> DEFAULT_EDUCATIONS =
    [
        new()
        {
            Id = 1,
            School = "NetCV School",
            Title = "NetCV Title",
            StartDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-1)),
            EndDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1))
        },
        new()
        {
            Id = 2,
            School = "BMS Zürich",
            Title = "Berufsmatura",
            StartDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-5)),
            EndDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-1))
        }
    ];

    public static readonly EducationDto DEFAULT_EDUCATION_DTO = new()
    {
        Id = 1,
        School = "NetCV School",
        Title = "NetCV Title",
        StartDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-1)),
        EndDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1))
    };
}
