using General.Basics.Bounds.Exceptions;
using General.Basics.Bounds.Intervals.Abstracts;

namespace General.Basics.Bounds.Intervals;

public record DateTimesInterval : IntervalBase<DateTime>
{
    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    public DateTimesInterval(MinAndMax<DateTime> bounds) : base(bounds)
    {
    }

    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    public DateTimesInterval(DateTime? minValue, DateTime? maxValue) : this(new MinAndMax<DateTime>(minValue, maxValue))
    {
    }

    /// <summary>
    /// Retourne l'intervalle (DateTimesInterval), représentant l'intersection entre this et dateTimesInterval.
    /// </summary>
    /// <returns>null si pas d'intersection</returns>
    public DateTimesInterval? GetIntersection(DateTimesInterval dateTimesInterval)
    {
        MinAndMax<DateTime>? intersection = GetIntersectionMinAndMax(dateTimesInterval);
        DateTimesInterval? result = intersection is null ? null : new DateTimesInterval(intersection.MinValue, intersection.MaxValue);
        return result;
    }
}
