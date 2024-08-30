using Domain.Models;

namespace Tests.DummyData;

public class ContentData
{
    public static readonly IEnumerable<Content> DEFAULT_CONTENTS =
    [
        new()
        {
            Key = "Name",
            Value = "NetCV"
        },
        new()
        {
            Key = "Language",
            Value = "C#"
        }
    ];
}
