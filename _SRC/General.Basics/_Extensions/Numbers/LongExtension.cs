using General.Basics.ErrorHandling;

namespace General.Basics.Extensions;

public static class LongExtension
{
    private static readonly Dictionary<long, long> factorialsCache = new(); //for optimization (to save computing time)

    /// <exception cref="LongShouldBeGreaterOrEqualException"></exception>
    public static void CheckIsGreaterOrEqualTo_(this long @long, long minimalValue, string subject = "long int")
    {
        if (@long < minimalValue)
        {
            throw new LongShouldBeGreaterOrEqualException(subject, @long, minimalValue);
        }
    }

    /// <exception cref="LongShouldBeGreaterOrEqualException"></exception>
    public static long GetNbAllOrderedCombinations_(this long nbElements)
    {
        nbElements.CheckIsGreaterOrEqualTo_(0L, nameof(nbElements));

        long result = 0L;
        for (long groupSize = 1; groupSize <= nbElements; groupSize++)
        {
            result += nbElements.GetNbOrderedCombinationsOf_(groupSize);
        }
        return result;
    }

    /// <exception cref="LongShouldBeGreaterOrEqualException"></exception>
    public static long GetNbOrderedCombinationsOf_(this long nbElements, long groupSize)
    {
        groupSize.CheckIsGreaterOrEqualTo_(0L, nameof(groupSize));
        nbElements.CheckIsGreaterOrEqualTo_(groupSize, nameof(nbElements));

        var result = (nbElements > 0L && groupSize > 0L) ? nbElements.Factorial_() / (nbElements - groupSize).Factorial_() : 0L;
        return result;
    }



    /// <exception cref="LongShouldBeGreaterOrEqualException"></exception>
    public static long GetNbAllUnOrderedCombinations_(this long nbElements)
    {
        nbElements.CheckIsGreaterOrEqualTo_(0L, nameof(nbElements));

        long result = 0L;
        for (long groupSize = 1; groupSize <= nbElements; groupSize++)
        {
            result += nbElements.GetNbUnOrderedCombinationsOf_(groupSize);
        }
        return result;
    }

    /// <exception cref="LongShouldBeGreaterOrEqualException"></exception>
    public static long GetNbUnOrderedCombinationsOf_(this long nbElements, long groupSize)
    {
        groupSize.CheckIsGreaterOrEqualTo_(0L, nameof(groupSize));
        nbElements.CheckIsGreaterOrEqualTo_(groupSize, nameof(nbElements));

        var result = (nbElements > 0L && groupSize > 0L)
                    ? nbElements.Factorial_() / (groupSize.Factorial_() * (nbElements - groupSize).Factorial_())
                    : 0L;

        return result;
    }

    /// <exception cref="LongShouldBeGreaterOrEqualException"></exception>
    public static long Factorial_(this long number)
    {
        long result = factorialsCache.GetValueOrDefault(number);
        if (result == default)
        {
            number.CheckIsGreaterOrEqualTo_(0L, nameof(number));

            result = (number == 0L) ? 1 : number * (number - 1L).Factorial_();
            factorialsCache.Add(number, result);
        }
        return result;
    }
}
