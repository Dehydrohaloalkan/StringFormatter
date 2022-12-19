using StringFormatter.Core.States.Base;

namespace StringFormatter.Core.States;

internal class CloseBracketState : State
{
    public override State GetNext(StateMachineContext context, char token)
    {
        if (token != '}')
        {
            throw new InvalidOperationException("Invalid '}' token has been detected.");
        }

        context.Append('}');
        return new InitialState();
    }
}