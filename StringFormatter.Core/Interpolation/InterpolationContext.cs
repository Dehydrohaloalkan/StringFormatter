using System.Collections;
using StringFormatter.Core.Interpolation.Base;

namespace StringFormatter.Core.Interpolation;

internal class InterpolationContext : IEnumerable<IInterpolationMember>
{
    private readonly List<IInterpolationMember> _items = new();

    public void Add(IInterpolationMember item)
    {
        _items.Add(item);
    }

    public IEnumerator<IInterpolationMember> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    public override string ToString()
    {
        return string.Join(".", _items);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}