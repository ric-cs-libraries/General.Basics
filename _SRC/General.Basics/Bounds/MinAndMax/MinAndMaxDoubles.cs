using General.Basics.Bounds.Exceptions;
using General.Basics.Bounds.MinAndMax;
using General.Basics.Extensions;

namespace General.Basics.Bounds.MinAndMax;

public record MinAndMaxDoubles : MinAndMax<double>
{
    public const double MIN_VALUE = Double.MinValue;
    public const double MAX_VALUE = Double.MaxValue;

    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    /// <param name="minValue">If not provided will be Equal to MinAndMaxDoubles.MIN_VALUE</param>
    /// <param name="maxValue">If not provided will be Equal to MinAndMaxDoubles.MAX_VALUE</param>
    public MinAndMaxDoubles(double? minValue = null, double? maxValue = null) :
        base(minValue.HasValue ? minValue.Value : MIN_VALUE, maxValue.HasValue ? maxValue.Value : MAX_VALUE)
    {
    }
}