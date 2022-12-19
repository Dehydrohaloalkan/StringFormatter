using StringFormatter.Core.States.Base;

namespace StringFormatter.Core.States;

internal class InitialState : State
{
    public override State GetNext(StateMachineContext context, char token)
    {
        switch (token)
        {
            case '{':
                return new OpenBracketState();
            case '}':
                return new CloseBracketState();
            default:
            {
                context.Append(token);
                return this;
            }
        }
    }
}