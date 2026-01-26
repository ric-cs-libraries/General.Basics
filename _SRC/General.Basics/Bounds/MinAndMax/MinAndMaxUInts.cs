using General.Basics.Bounds.Exceptions;
using General.Basics.Bounds.MinAndMax;
using General.Basics.Extensions;

namespace General.Basics.Bounds.MinAndMax;

public record MinAndMaxUInts : MinAndMax<uint>
{
    public const uint MIN_VALUE = UInt32.MinValue; //0
    public const uint MAX_VALUE = UInt32.MaxValue;

    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    /// <param name="minValue">If not provided will be Equal to MinAndMaxUInts.MIN_VALUE</param>
    /// <param name="maxValue">If not provided will be Equal to MinAndMaxUInts.MAX_VALUE</param>
    public MinAndMaxUInts(uint? minValue = null, uint? maxValue = null) :
        base(minValue.HasValue ? minValue.Value : MIN_VALUE, maxValue.HasValue ? maxValue.Value : MAX_VALUE)
    {
    }
}