using StringFormatter.Core.Interfaces;
using StringFormatter.Core.States.Base;
using StringFormatter.Core.States;
using System.Reflection.PortableExecutable;
using StringFormatter.Core.MemberReader.Base;
using StringFormatter.Core.MemberReader;

namespace StringFormatter.Core;

public class StringFormatter : IStringFormatter 
{
    private static StringFormatter _shared = new();
    public static StringFormatter Shared => _shared;

    private readonly IMemberReader _reader;

    public StringFormatter()
    {
        _reader = new CachedMemberReader();
    }

    public string Format(string template, object value)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));
        ArgumentNullException.ThrowIfNull(template, nameof(template));

        var context = new StateMachineContext(_reader, template, value);

        State state = new InitialState();

        foreach (var token in template)
        {
            state = state.GetNext(context, token);
            context.MoveNext();
        }

        return context.Result;
    }
}