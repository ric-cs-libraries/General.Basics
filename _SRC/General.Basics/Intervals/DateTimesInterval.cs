using General.Basics.Intervals.Abstracts;
using General.Basics.Intervals.Exceptions;


namespace General.Basics.Intervals;

public record DateTimesInterval : IntervalBase<DateTime?>
{
    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}"></exception>
	public DateTimesInterval(DateTime? minValue, DateTime? maxValue) : base(minValue, maxValue)
	{
    }

    /// <summary>
    /// Retourne true si : value1.Value &lt;= value2.Value.
    /// </summary>
    protected override bool IsLowerOrEqual(DateTime? value1, DateTime? value2) => value1!.Value <= value2!.Value;
}
