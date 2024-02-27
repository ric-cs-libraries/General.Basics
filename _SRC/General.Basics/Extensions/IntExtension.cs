using General.Basics.Exceptions;

namespace General.Basics.Extensions;

public static class IntExtension
{
    public static void CheckIsGreaterOrEqualTo_(this int int_, int minimalValue, string subject = "number")
    {
        if (int_ < minimalValue)
        {
            throw new IntShouldBeGreaterOrEqualException(subject, int_, minimalValue);
        }
    }

}
