using StringFormatter.Core.States.Base;

namespace StringFormatter.Core.States;

internal class InterpolationState : State
{
    private int _startIndex;
    private readonly InterpolationContext _context;

    private int _startArrayIndex;
    private bool _isArray;

    public InterpolationState(int startIndex)
    {
        _startIndex = startIndex;
        _context = new InterpolationContext();
    }

    public override State GetNext(StateMachineContext context, char token)
    {
        switch (token)
        {
            case '.':
                {
                    SaveCurrentItem(context);
                    return this;
                }
            case '[':
                {
                    _startArrayIndex = context.Index - 1;
                    return new ArrayIndexState(this);
                }
            case '{':
                throw new InvalidOperationException("Invalid open brackets count.");
            case '}':
                {
                    SaveCurrentItem(context);

                    if (!context.TryGetObjectValue(_context, out var value))
                    {
                        throw new InvalidOperationException("Invalid member's name.");
                    }

                    context.Append(value);
                    return new InitialState();
                }
            default:
                return this;
        }
    }

    private void SaveCurrentItem(StateMachineContext context)
    {
        if (!_isArray)
        {
            var item = new InterpolationMember(context.Template.Substring(_startIndex,
                context.Index - _startIndex));
            _startIndex = context.Index + 1;

            _context.Add(item);
        }

        _isArray = false;
    }

    public void SetArrayIndex(StateMachineContext context, int index)
    {
        var member = new InterpolationArrayMember(
            context.Template.Substring(_startIndex, _startArrayIndex - _startIndex + 1),
            index);
        _startIndex = context.Index + 2;

        _context.Add(member);

        _isArray = true;
    }
}
