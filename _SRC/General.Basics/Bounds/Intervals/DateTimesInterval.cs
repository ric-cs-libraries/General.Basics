using General.Basics.Bounds.Exceptions;
using General.Basics.Bounds.Intervals.Abstracts;
using General.Basics.Bounds.MinAndMax;

namespace General.Basics.Bounds.Intervals;

public record DateTimesInterval : IntervalBase<DateTime>
{
    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    /// <param name="minValue">If not provided will be Equal to MinAndMaxDateTimes.MIN_VALUE</param>
    /// <param name="maxValue">If not provided will be Equal to MinAndMaxDateTimes.MAX_VALUE</param>       
    public DateTimesInterval(DateTime? minValue, DateTime? maxValue) : base(new MinAndMaxDateTimes(minValue, maxValue))
    {
    }

    /// <summary>
    /// Retourne l'intervalle (DateTimesInterval), représentant l'intersection entre this et dateTimesInterval.
    /// </summary>
    /// <returns>null si pas d'intersection</returns>
    public DateTimesInterval? GetIntersection(DateTimesInterval dateTimesInterval)
    {
        MinAndMax<DateTime>? intersection = GetIntersectionMinAndMax(dateTimesInterval);
        bool existsIntersection = (intersection is not null);
        DateTimesInterval? result = existsIntersection ? new DateTimesInterval(intersection!.MinValue, intersection!.MaxValue) : null;
        return result;
    }
}
