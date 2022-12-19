using StringFormatter.Core.Interfaces;

namespace StringFormatter.Core;

public class StringFormatter : IStringFormatter 
{
    private static StringFormatter _shared = new();
    public static StringFormatter Shared => _shared;

    public string Format(string template, object target)
    {
        
    }
}