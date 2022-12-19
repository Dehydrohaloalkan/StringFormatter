using StringFormatter.Core.States.Base;

namespace StringFormatter.Core.States;

internal class OpenBracketState : State
{
    public override State GetNext(StateMachineContext context, char token)
    {
        switch (token)
        {
            case '{':
                context.Append('{');
                return new InitialState();
            default:
                return new InterpolationState(context.Index);
        }
    }
}