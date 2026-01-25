namespace General.Basics.Extensions;

public static class IComparableExtension
{
    public static bool IsGreaterThan<T>(this T comparable, T value)
        where T : struct, IComparable<T> // struct => type valeur non nullable
       => comparable.CompareTo(value) > 0;

    public static bool IsLowerThan<T>(this T comparable, T value)
        where T : struct, IComparable<T> // struct => type valeur non nullable
       => comparable.CompareTo(value) < 0;

    public static bool IsEqualTo<T>(this T comparable, T value)
        where T : struct, IComparable<T> // struct => type valeur non nullable
       => comparable.CompareTo(value) == 0;

    public static bool IsGreaterOrEqualTo<T>(this T comparable, T value)
        where T : struct, IComparable<T> // struct => type valeur non nullable
       => comparable.IsEqualTo(value) || comparable.IsGreaterThan(value);

    public static bool IsLowerOrEqualTo<T>(this T comparable, T value)
        where T : struct, IComparable<T> // struct => type valeur non nullable
       => comparable.IsEqualTo(value) || comparable.IsLowerThan(value);
}
