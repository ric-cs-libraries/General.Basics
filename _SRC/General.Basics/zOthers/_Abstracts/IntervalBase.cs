using General.Basics.Others.Exceptions;

namespace General.Basics.Others.Abstracts;

public abstract record IntervalBase<T>
{

    public T MinValue { get; }
    public T MaxValue { get; }

    protected abstract bool IsLowerOrEqual(T value1, T value2);

    public IntervalBase(T minValue, T maxValue)
	{
        if (!IsLowerOrEqual(minValue, maxValue))
        {
            throw new ValueShouldBeLowerOrEqualToException<T>("Interval minValue", minValue, maxValue);
        }
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public bool Contains(T value)
    {
        var result = IsLowerOrEqual(MinValue, value) && IsLowerOrEqual(value, MaxValue);
        return result;
    }

}
