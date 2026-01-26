using General.Basics.Bounds.Exceptions;
using General.Basics.Extensions;

namespace General.Basics.Bounds.MinAndMax;

public record MinAndMax<T>
    where T : struct, IComparable<T> // struct => type valeur non nullable
{
    public T MinValue { get; }
    public T MaxValue { get; }

    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    public MinAndMax(T minValue, T maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;

        if (minValue.IsGreaterThan(maxValue))
        {
            throw new ValueShouldBeLowerOrEqualToException<T>("minValue", minValue, maxValue);
        }
    }
}