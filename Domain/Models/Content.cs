using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Domain.Models;

[Index(nameof(Key), IsUnique = true)]
public class Content
{
    public long Id { get; set; }

    public required string Key { get; set; }

    public required string Value { get; set; }
}
