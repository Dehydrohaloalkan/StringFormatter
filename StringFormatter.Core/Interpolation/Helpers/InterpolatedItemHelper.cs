using System.Linq.Expressions;
using StringFormatter.Core.Interpolation.Base;

namespace StringFormatter.Core.Interpolation.Helpers;

internal static class InterpolatedItemHelper
{
    public static Expression AddToExpression(this Expression expression, IInterpolationMember member)
    {
        switch (member)
        {
            case InterpolationMember:
                return Expression.PropertyOrField(expression, member.MemberName);
            case InterpolationArrayMember arrayMember:
            {
                var propertyExpression = Expression.PropertyOrField(expression, member.MemberName);
                var indexExpression = Expression.Constant(arrayMember.Index, typeof(int));
                return Expression.ArrayIndex(propertyExpression, indexExpression);
            }
            default:
                return expression;
        }
    }

    public static Expression AddToExpression(this Expression expression, InterpolationContext context)
    {
        return context.Aggregate(expression, (current, item) => current.AddToExpression(item));
    }
}