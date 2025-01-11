using General.Basics.Intervals.Abstracts;


namespace General.Basics.Intervals;

public record DateTimesInterval : IntervalBase<DateTime?>
{
	public DateTimesInterval(DateTime? minValue, DateTime? maxValue) : base(minValue, maxValue)
	{
    }

    protected override bool IsLowerOrEqual(DateTime? value1, DateTime? value2) => value1!.Value <= value2!.Value;
}
