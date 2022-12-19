using StringFormatter.Core.Interpolation.Base;

namespace StringFormatter.Core.Interpolation;

public class InterpolationArrayMember : IInterpolationMember
{
    private int _index;

    public int Index
    {
        get => _index;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Index cannot be less than 0.", nameof(Index));
            }

            _index = value;
        }
    }

    public string MemberName { get; }

    public InterpolationArrayMember(string memberName, int index)
    {
        if (string.IsNullOrEmpty(memberName))
        {
            throw new ArgumentNullException(nameof(memberName));
        }

        MemberName = memberName;
        Index = index;
    }

    public override string ToString() => $"{MemberName}[{Index}]";
}