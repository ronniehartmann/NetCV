using Application.Services.Resources.Models;
using Domain.Models;

namespace Application.Dtos;

public class EducationDto : Resource<Education>
{
    public long Id { get; set; }

    public string School { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public override bool IsEqualToModel(Education model)
    {
        return string.Equals(School, model.School)
            && string.Equals(Title, model.Title)
            && StartDate == model.StartDate
            && EndDate == model.EndDate;
    }
}
