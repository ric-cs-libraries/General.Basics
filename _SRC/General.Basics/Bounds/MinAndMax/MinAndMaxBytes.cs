using General.Basics.Bounds.Exceptions;
using General.Basics.Bounds.MinAndMax;
using General.Basics.Extensions;

namespace General.Basics.Bounds.MinAndMax;

public record MinAndMaxBytes : MinAndMax<byte>
{
    public const byte MIN_VALUE = Byte.MinValue; //0
    public const byte MAX_VALUE = Byte.MaxValue; //255

    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    /// <param name="minValue">If not provided will be Equal to MinAndMaxBytes.MIN_VALUE</param>
    /// <param name="maxValue">If not provided will be Equal to MinAndMaxBytes.MAX_VALUE</param>
    public MinAndMaxBytes(byte? minValue = null, byte? maxValue = null) :
        base(minValue.HasValue ? minValue.Value : MIN_VALUE, maxValue.HasValue ? maxValue.Value : MAX_VALUE)
    {
    }
}