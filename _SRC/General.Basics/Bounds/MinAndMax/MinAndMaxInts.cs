using General.Basics.Bounds.Exceptions;
using General.Basics.Bounds.MinAndMax;
using General.Basics.Extensions;

namespace General.Basics.Bounds.MinAndMax;

public record MinAndMaxInts : MinAndMax<int>
{
    public const int MIN_VALUE = Int32.MinValue;
    public const int MAX_VALUE = Int32.MaxValue;

    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    /// <param name="minValue">If not provided will be Equal to MinAndMaxInts.MIN_VALUE</param>
    /// <param name="maxValue">If not provided will be Equal to MinAndMaxInts.MAX_VALUE</param>
    public MinAndMaxInts(int? minValue = null, int? maxValue = null) :
        base(minValue.HasValue ? minValue.Value : MIN_VALUE, maxValue.HasValue ? maxValue.Value : MAX_VALUE)
    {
    }
}