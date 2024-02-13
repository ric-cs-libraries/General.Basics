using Transverse._Common.General.Basics.Exceptions;

namespace Transverse._Common.General.Basics.Extensions;

public static class IEnumerableExtension
{
    public static bool IsValidIndex_<T>(this IEnumerable<T> enumerable, int index)
    {
        int minIndexValue = 0;
        int maxIndexValue = enumerable.Count() - 1;
        var result = (index >= minIndexValue && index <= maxIndexValue);
        return result;
    }

    public static void CheckIsValidIndex_<T>(this IEnumerable<T> enumerable, int index)
    {
        if (!enumerable.IsValidIndex_(index))
        {
            int minIndexValue = 0;
            int maxIndexValue = enumerable.Count() - 1;

            throw new OutOfRangeIntegerException(index, minIndexValue, maxIndexValue, "IEnumerable Index");
        }
    }
}
