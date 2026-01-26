using General.Basics.Bounds.Exceptions;
using General.Basics.Bounds.MinAndMax;
using General.Basics.Extensions;

namespace General.Basics.Bounds.MinAndMax;

public record MinAndMaxDateTimes : MinAndMax<DateTime>
{
    public static readonly DateTime MIN_VALUE = DateTime.MinValue; //01/01/0001 00:00:00
    public static readonly DateTime MAX_VALUE = DateTime.MaxValue; //31/12/9999 00:00:00

    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    /// <param name="minValue">If not provided will be Equal to MinAndMaxDateTimes.MIN_VALUE</param>
    /// <param name="maxValue">If not provided will be Equal to MinAndMaxDateTimes.MAX_VALUE</param>    
    public MinAndMaxDateTimes(DateTime? minValue = null, DateTime? maxValue = null) : 
        base(minValue.HasValue ? minValue.Value : MIN_VALUE, maxValue.HasValue ? maxValue.Value : MAX_VALUE)
    {
    }
}