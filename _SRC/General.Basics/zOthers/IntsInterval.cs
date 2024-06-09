using General.Basics.Others.Abstracts;

namespace General.Basics.Others;


public record IntsInterval : IntervalBase<int>
{
	public IntsInterval(int minValue, int maxValue) : base(minValue, maxValue)
	{
    }

    protected override bool IsLowerOrEqual(int value1, int value2) => value1 <= value2;
}
