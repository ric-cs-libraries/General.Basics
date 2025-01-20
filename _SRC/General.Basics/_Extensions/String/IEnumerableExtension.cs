namespace General.Basics.Extensions;

public static partial class IEnumerableExtension
{
    public static string ToString_(this IEnumerable<IEnumerable<char>> charss)
    {
        return charss.Select(chars => chars.ToString_()).ToString_();
    }

    public static string ToString_(this IEnumerable<char> chars)
    {
        return string.Join("", chars);
    }
    public static string ToString_(this IEnumerable<string> strings)
    {
        return string.Join("", strings);
    }

    public static string ToStringAsArray_(this IEnumerable<string> strings)
    {
        string stringsAsArray = $"['{string.Join("', '", strings)}']";
        return stringsAsArray;
    }

    public static string ToStringAsArray_(this IEnumerable<char> chars)
    {
        string charsAsArray = $"['{string.Join("', '", chars)}']";
        return charsAsArray;
    }
}
