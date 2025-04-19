using General.Basics.Intervals.Exceptions;


namespace General.Basics.Intervals.Abstracts;

public abstract record IntervalBase<T>
{

    public T MinValue { get; }
    public T MaxValue { get; }

    public bool IsMin => MinValue is not null;
    public bool IsMax => MaxValue is not null;

    protected abstract bool IsLowerOrEqual(T value1, T value2);

    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}"></exception>
    public IntervalBase(T minValue, T maxValue)
	{
        MinValue = minValue;
        MaxValue = maxValue;

        if (IsMin && IsMax && !IsLowerOrEqual(minValue, maxValue))
        {
            throw new ValueShouldBeLowerOrEqualToException<T>("Interval minValue", minValue, maxValue);
        }

    }

    public bool Contains(T value)
    {
        var result = (!IsMin || IsLowerOrEqual(MinValue, value)) && (!IsMax || IsLowerOrEqual(value, MaxValue));
        return result;
    }

}
