using StringFormatter.Core.Interpolation;
using StringFormatter.Core.MemberReader.Base;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using StringFormatter.Core.Interpolation.Helpers;

namespace StringFormatter.Core.MemberReader;

internal class CachedMemberReader : IMemberReader
{
    private readonly ConcurrentDictionary<string, Delegate> _dictionary = new();

    public bool TryGetObjectValue(object target, InterpolationContext context, out string value)
    {
        var name = $"{target.GetType()}.{context}";
        if (_dictionary.ContainsKey(name))
        {
            value = _dictionary[name].DynamicInvoke(target)?.ToString() ?? "null";
            return true;
        }

        return TryGetMemberThroughReflection(target, context, out value);
    }

    private bool TryGetMemberThroughReflection(object target, InterpolationContext context, out string value)
    {
        var parameterExpression = Expression.Parameter(target.GetType());
        var bodyExpression = parameterExpression.AddToExpression(context);
        var lambdaExpression = Expression.Lambda(bodyExpression, parameterExpression);

        try
        {
            var a = lambdaExpression.Compile();
            value = a.DynamicInvoke(target)?.ToString() ?? "null";
            _dictionary[$"{target.GetType()}.{context}"] = a;

            return true;
        }
        catch
        {
            value = "";
            return false;
        }
    }
}