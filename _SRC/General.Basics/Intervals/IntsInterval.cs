using General.Basics.Intervals.Abstracts;


namespace General.Basics.Intervals;

public record IntsInterval : IntervalBase<int?>
{
	public IntsInterval(int? minValue, int? maxValue) : base(minValue, maxValue)
	{
    }

    /// <summary>
    /// Retourne true si : value1.Value &lt;= value2.Value.
    /// </summary>
    protected override bool IsLowerOrEqual(int? value1, int? value2) => value1!.Value <= value2!.Value;
}
