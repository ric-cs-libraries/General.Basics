using System.Web;

using Transverse._Common.General.Basics.Exceptions;

namespace Transverse._Common.General.Basics.Extensions;

public static class StringExtension
{
    public static bool IsValidIndex_(this string string_, int index)
    {
        int minIndex = 0;
        int maxIndex = string_.Length - 1;
        var result = (index >= minIndex && index <= maxIndex);
        return result;
    }

    public static void CheckIsValidIndex_(this string string_, int index)
    {
        if (!string_.IsValidIndex_(index))
        {
            int minIndex = 0;
            int maxIndex = string_.Length - 1;

            throw new OutOfRangeIntegerException(index, minIndex, maxIndex, "string Index");
        }
    }

    public static bool ChunkExists_(this string string_, int startIndex, int endIndex)
    {
        var result = (endIndex >= startIndex);
        result = result && string_.IsValidIndex_(startIndex) && string_.IsValidIndex_(endIndex);
        return result;
    }

    public static void CheckChunkExists_(this string string_, int startIndex, int endIndex)
    {
        if (!string_.ChunkExists_(startIndex, endIndex))
        {
            int minIndex = 0;
            int maxIndex = string_.Length - 1;

            throw new UnexistingChunkException(startIndex, endIndex, minIndex, maxIndex, "string");
        }
    }

    public static string GetChunk_(this string string_, int startIndex, int endIndex)
    {
        string_.CheckChunkExists_(startIndex, endIndex);
        var chunkLength = endIndex - startIndex + 1;
        var result = string_.AsSpan(startIndex, chunkLength).ToString();
        return result;
    }

    public static string Repeat_(this string string_, int nbRepeat)
    {
        if (nbRepeat < 0)
        {
            throw new MustBePositiveIntegerException(nbRepeat, nameof(nbRepeat));
        }
        char[] chars = string_.ToArray();
        char[] resultChars = { };
        foreach (int _ in Enumerable.Range(1, nbRepeat))
        {
            resultChars = resultChars.Concat(chars).ToArray();
        }
        var str = new string(resultChars);
        return str;
    }

    public static string EndsWith_(this string string_, bool mustEndWith, string end)
    {
        var retour = string_;
        var endsWith = string_.EndsWith(end);

        if (!endsWith && mustEndWith)
        {
            retour += end;
        }

        else if (endsWith && !mustEndWith)
        {
            retour = retour.AsSpan(0, retour.Length - end.Length).ToString();
        }

        return (retour);
    }

    public static string StartsWith_(this string string_, bool mustStartWith, string start)
    {
        var retour = string_;
        var startsWith = string_.StartsWith(start);

        if (!startsWith && mustStartWith)
        {
            retour = start + retour;
        }

        else if (startsWith && !mustStartWith)
        {
            retour = retour.AsSpan(start.Length).ToString();
        }

        return (retour);
    }

    public static string Substring_(this string string_, int startIndex, int substringLength)
    {
        string retour = string.Empty;

        if (startIndex < 0)
        {
            throw new MustBePositiveIntegerException(startIndex, "The index");
        }

        if (startIndex < string_.Length)
        {
            if (startIndex + substringLength > string_.Length)
            {
                substringLength = string_.Length - startIndex;
            }
            retour = string_.AsSpan(startIndex, substringLength).ToString();
        }

        return (retour);
    }
    public static string Substring_(this string string_, int startIndex)
    {
        if (startIndex < 0)
        {
            throw new MustBePositiveIntegerException(startIndex, "The index");
        }
        var retour = (startIndex >= string_.Length) ? string.Empty : string_.AsSpan(startIndex).ToString();
        return (retour);
    }

    public static string GetAsShorten_(this string str, int maxLength, string suffix = "...")
    {
        if (maxLength > 0)
        {
            if (str.Length > maxLength)
            {
                int chunkLength = maxLength - suffix.Length;

                return (chunkLength > 0) ? str.AsSpan(0, chunkLength).ToString() + suffix : str.AsSpan(0, maxLength).ToString();
            }

            return str;
        }
        return string.Empty;
    }

    public static string GetWithHtmlEntities_(this string str)
    {
        return HttpUtility.HtmlEncode(str);
    }

    public static string GetWithoutHtmlEntities_(this string str)
    {
        return HttpUtility.HtmlDecode(str);
    }
}