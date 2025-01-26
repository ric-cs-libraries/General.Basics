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
        string stringsAsArray = (strings.Any()) ? $"'{string.Join("', '", strings)}'" : string.Empty;
        return $"[{stringsAsArray}]";
    }

    public static string ToStringAsArray_(this IEnumerable<char> chars)
    {
        string charsAsArray = (chars.Any()) ? $"'{string.Join("', '", chars)}'" : string.Empty;
        return $"[{charsAsArray}]";
    }
}
