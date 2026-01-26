using General.Basics.Bounds.Exceptions;
using General.Basics.Bounds.Intervals.Abstracts;
using General.Basics.Bounds.MinAndMax;

namespace General.Basics.Bounds.Intervals;

public record UIntsInterval : IntervalBase<uint>
{
    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    /// <param name="minValue">If not provided will be Equal to MinAndMaxUInts.MIN_VALUE</param>
    /// <param name="maxValue">If not provided will be Equal to MinAndMaxUInts.MAX_VALUE</param>    
    public UIntsInterval(uint? minValue, uint? maxValue) : base(new MinAndMaxUInts(minValue, maxValue))
    {
    }

    /// <summary>
    /// Retourne l'intervalle (UIntsInterval), représentant l'intersection entre this et uintsInterval.
    /// </summary>
    /// <returns>null si pas d'intersection</returns>
    public UIntsInterval? GetIntersection(UIntsInterval uintsInterval)
    {
        MinAndMax<uint>? intersection = GetIntersectionMinAndMax(uintsInterval);
        bool existsIntersection = (intersection is not null);
        UIntsInterval? result = existsIntersection ? new UIntsInterval(intersection!.MinValue, intersection!.MaxValue) : null;
        return result;
    }
}
