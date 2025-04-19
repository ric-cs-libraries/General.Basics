using General.Basics.ErrorHandling;

namespace General.Basics.Extensions;

public static class IntExtension
{
    /// <exception cref="IntShouldBeGreaterOrEqualException"></exception>
    public static void CheckIsGreaterOrEqualTo_(this int int_, int minimalValue, string subject = "number")
    {
        if (int_ < minimalValue)
        {
            throw new IntShouldBeGreaterOrEqualException(subject, int_, minimalValue);
        }
    }

    public static bool IsBetween_(this int int_, int lowBoundIncluded, int highBoundIncluded)
    {
        highBoundIncluded.CheckIsGreaterOrEqualTo_(lowBoundIncluded, "highBoundIncluded");
        return (int_ >= lowBoundIncluded && int_ <= highBoundIncluded);
    }

    public static bool IsInRange_(this int int_, Range range) //Range : [start,end[
    {
        return int_.IsBetween_(range.Start.Value, range.End.Value-1);
    }


    public static bool IsDivisibleBy_(this int int_, int divider)
    {
        var result = (divider != 0) && (int_ % divider == 0);
        return result;
    }

    public static bool IsPair_(this int int_)
    {
        return int_.IsDivisibleBy_(2);
    }
    public static bool IsOdd_(this int int_)
    {
        return !int_.IsPair_();
    }

}
