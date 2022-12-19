namespace StringFormatter.Core.States.Base;

internal abstract class State
{
    public abstract State GetNext(StateMachineContext context, char token);
}