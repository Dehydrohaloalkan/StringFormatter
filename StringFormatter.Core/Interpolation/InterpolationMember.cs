using StringFormatter.Core.Interpolation.Base;

namespace StringFormatter.Core.Interpolation;

public class InterpolationMember : IInterpolationMember
{
    public string MemberName { get; }

    public InterpolationMember(string memberName)
    {
        if (string.IsNullOrEmpty(memberName))
        {
            throw new ArgumentNullException(nameof(memberName));
        }

        MemberName = memberName;
    }

    public override string ToString() => MemberName;
}