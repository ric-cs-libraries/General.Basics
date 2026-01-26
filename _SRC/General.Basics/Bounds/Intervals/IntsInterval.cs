using General.Basics.Bounds.Exceptions;
using General.Basics.Bounds.Intervals.Abstracts;
using General.Basics.Bounds.MinAndMax;

namespace General.Basics.Bounds.Intervals;

public record IntsInterval : IntervalBase<int>
{
    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    /// <param name="minValue">If not provided will be Equal to MinAndMaxInts.MIN_VALUE</param>
    /// <param name="maxValue">If not provided will be Equal to MinAndMaxInts.MAX_VALUE</param>    
    public IntsInterval(int? minValue, int? maxValue) : base(new MinAndMaxInts(minValue, maxValue))
    {
    }

    /// <summary>
    /// Retourne l'intervalle (IntsInterval), représentant l'intersection entre this et intsInterval.
    /// </summary>
    /// <returns>null si pas d'intersection</returns>
    public IntsInterval? GetIntersection(IntsInterval intsInterval)
    {
        MinAndMax<int>? intersection = GetIntersectionMinAndMax(intsInterval);
        bool existsIntersection = (intersection is not null);
        IntsInterval? result = existsIntersection ? new IntsInterval(intersection!.MinValue, intersection!.MaxValue) : null;
        return result;
    }
}
