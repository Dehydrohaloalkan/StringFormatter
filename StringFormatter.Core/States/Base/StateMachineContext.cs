using System.Text;

namespace StringFormatter.Core.States.Base;

internal class StateMachineContext
{
    private readonly IMemberReader _memberReader;
    private readonly StringBuilder _stringBuilder = new();

    public string Template { get; }
    public object Target { get; }
    public int Index { get; private set; }

    public StateMachineContext(IMemberReader memberReader, string template, object target)
    {
        _memberReader = memberReader;
        Template = template;
        Target = target;
        Index = 0;
    }

    public string Result => _stringBuilder.ToString();

    public void Append(string value) => _stringBuilder.Append(value);

    public void Append(char value) => _stringBuilder.Append(value);

    public bool TryGetObjectValue(InterpolationContext context, out string value)
    {
        return _memberReader.TryGetObjectValue(Target, context, out value);
    }

    public void MoveNext() => Index++;
}