using StringFormatter.Core.Interpolation;

namespace StringFormatter.Core.MemberReader.Base;

internal interface IMemberReader
{
    bool TryGetObjectValue(object target, InterpolationContext context, out string value);
}