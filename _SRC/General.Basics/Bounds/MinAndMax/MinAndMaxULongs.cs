using General.Basics.Bounds.Exceptions;
using General.Basics.Bounds.MinAndMax;
using General.Basics.Extensions;

namespace General.Basics.Bounds.MinAndMax;

public record MinAndMaxULongs : MinAndMax<ulong>
{
    public const ulong MIN_VALUE = UInt64.MinValue; //0
    public const ulong MAX_VALUE = UInt64.MaxValue;

    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    /// <param name="minValue">If not provided will be Equal to MinAndMaxULongs.MIN_VALUE</param>
    /// <param name="maxValue">If not provided will be Equal to MinAndMaxULongs.MAX_VALUE</param>
    public MinAndMaxULongs(ulong? minValue = null, ulong? maxValue = null) :
        base(minValue.HasValue ? minValue.Value : MIN_VALUE, maxValue.HasValue ? maxValue.Value : MAX_VALUE)
    {
    }
}