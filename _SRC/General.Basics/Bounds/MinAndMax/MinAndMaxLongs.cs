using General.Basics.Bounds.Exceptions;
using General.Basics.Bounds.MinAndMax;
using General.Basics.Extensions;

namespace General.Basics.Bounds.MinAndMax;

public record MinAndMaxLongs : MinAndMax<long>
{
    public const long MIN_VALUE = Int64.MinValue;
    public const long MAX_VALUE = Int64.MaxValue;

    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    /// <param name="minValue">If not provided will be Equal to MinAndMaxLongs.MIN_VALUE</param>
    /// <param name="maxValue">If not provided will be Equal to MinAndMaxLongs.MAX_VALUE</param>
    public MinAndMaxLongs(long? minValue = null, long? maxValue = null) :
        base(minValue.HasValue ? minValue.Value : MIN_VALUE, maxValue.HasValue ? maxValue.Value : MAX_VALUE)
    {
    }
}