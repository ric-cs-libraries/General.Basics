using General.Basics.Bounds.Exceptions;
using General.Basics.Bounds.MinAndMax;
using General.Basics.Extensions;

namespace General.Basics.Bounds.MinAndMax;

public record MinAndMaxDecimals : MinAndMax<decimal>
{
    public const decimal MIN_VALUE = Decimal.MinValue;
    public const decimal MAX_VALUE = Decimal.MaxValue;

    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    /// <param name="minValue">If not provided will be Equal to MinAndMaxDecimals.MIN_VALUE</param>
    /// <param name="maxValue">If not provided will be Equal to MinAndMaxDecimals.MAX_VALUE</param>
    public MinAndMaxDecimals(decimal? minValue = null, decimal? maxValue = null) :
        base(minValue.HasValue ? minValue.Value : MIN_VALUE, maxValue.HasValue ? maxValue.Value : MAX_VALUE)
    {
    }

}