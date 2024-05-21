using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

[Index(nameof(Name), IsUnique = true)]
public class Skill
{
    public long Id { get; set; }

    public required string Name { get; set; }

    [Range(0, 100)]
    public int Level { get; set; }
}