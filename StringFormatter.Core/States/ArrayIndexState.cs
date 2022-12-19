using StringFormatter.Core.States.Base;

namespace StringFormatter.Core.States;

internal class ArrayIndexState : State
{
    private readonly InterpolationState _state;

    private int _index;

    public ArrayIndexState(InterpolationState state)
    {
        _state = state;
    }

    public override State GetNext(StateMachineContext context, char token)
    {
        if (char.IsDigit(token))
        {
            _index = _index * 10 + (int)(token - '0');
            return this;
        }

        if (token == ']')
        {
            _state.SetArrayIndex(context, _index);
            return _state;
        }

        throw new InvalidOperationException("Invalid token inside array index was found.");
    }
}