using Application.Dtos;
using Domain.Models;

namespace Tests.DummyData;

public static class ExperienceData
{
    public static readonly IEnumerable<Experience> DEFAULT_EXPERIENCES =
    [
        new()
        {
            Id = 1,
            StartDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-1)),
            EndDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
            Company = "NetCV",
            CompanyLink = "ronniehartmann.ch",
            Text = "Hello World!"
        },
        new()
        {
            Id = 2,
            StartDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-5)),
            EndDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-1)),
            Company = "Contoso",
            CompanyLink = "contoso.net"
        }
    ];

    public static readonly ExperienceDto DEFAULT_EXPERIENCE_DTO = new()
    {
        Id = 1,
        StartDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-1)),
        EndDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
        Company = "NetCV",
        CompanyLink = "ronniehartmann.ch",
        Text = "Hello World!"
    };
}
