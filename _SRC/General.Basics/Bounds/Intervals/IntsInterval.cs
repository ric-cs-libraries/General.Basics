using General.Basics.Bounds.Exceptions;
using General.Basics.Bounds.Intervals.Abstracts;

namespace General.Basics.Bounds.Intervals;

public record IntsInterval : IntervalBase<int>
{
    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    public IntsInterval(MinAndMax<int> bounds) : base(bounds)
    {
    }

    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    public IntsInterval(int? minValue, int? maxValue) : this(new MinAndMax<int>(minValue, maxValue))
    {
    }

    /// <summary>
    /// Retourne l'intervalle (IntsInterval), représentant l'intersection entre this et intsInterval.
    /// </summary>
    /// <returns>null si pas d'intersection</returns>
    public IntsInterval? GetIntersection(IntsInterval intsInterval)
    {
        MinAndMax<int>? intersection = GetIntersectionMinAndMax(intsInterval);
        IntsInterval? result = intersection is null ? null : new IntsInterval(intersection.MinValue, intersection.MaxValue);
        return result;
    }
}
