using General.Basics.ErrorHandling;
using System.Web;

namespace General.Basics.Extensions;



//ON RAPPELLE QU'une String est un IEnumerable<char>
public static class StringExtension
{
    private const int INDEX_WHEN_UNFOUND = -1;

    public static void CheckIsNotEqualToAnyOf_(this string str, IEnumerable<string> strings, StringComparison comparisonMode = StringComparison.InvariantCulture)
    {
        bool isEqual = str.EqualsOneOf_(strings, comparisonMode);
        if (isEqual)
        {
            throw new StringShouldNotBeEqualToAnyOfStringsException(comparisonMode, str, strings);
        }
    }

    public static bool EqualsOneOf_(this string string_, IEnumerable<string> strings, StringComparison comparisonMode = StringComparison.InvariantCulture)
    {
        string? foundString = strings.FirstOrDefault(str => str.Equals(string_, comparisonMode));
        bool result = (foundString is not null);
        return result;
    }

    public static List<string> SuffixWithNumberFromAToB_(this string str, int firstSuffixA, int lastSuffixB)
    {
        lastSuffixB.CheckIsGreaterOrEqualTo_(firstSuffixA, "index");

        var nbElements = lastSuffixB - firstSuffixA + 1;

        var result = Enumerable.Range(firstSuffixA, nbElements).Select(suffixN =>
        {
            return $"{str}{suffixN}";
        });
        return result.ToList();
    }

    public static void CheckDoesntContainIllegalChar_(this string str, char[] illegalChars)
    {
        int index;
        if ((index = str.IndexOfAny(illegalChars)) != INDEX_WHEN_UNFOUND)
        {
            throw new StringWithIllegalCharException(str, str[index]);
        }
    }

    public static void CheckDoesntContainTooManyOfAChar_(this string str, char theChar, int maxNbOccurrencesOfTheChar)
    {
        int charNbOccurences = str.Count(chr => chr == theChar);
        if (charNbOccurences > maxNbOccurrencesOfTheChar)
        {
            throw new StringContainsTooManyOfACharException(str, charNbOccurences, theChar, maxNbOccurrencesOfTheChar);
        }
    }

    public static void CheckDoesntOnlyContainSpaces_(this string str)
    {
        if (str.OnlyContains_(' '))
        {
            throw new StringOnlyContainsSpacesException(str);
        }
    }

    public static bool IsEmptyOrOnlySpaces_(this string str)
    {
        bool result = (str == string.Empty) || str.OnlyContains_(' ');
        return result;
    }

    public static bool IsEmpty_(this string str) => (str == string.Empty);

    public static bool OnlyContains_(this string str, char theOnlyCharToFind)
    {
        bool result = false;
        if (!str.IsEmpty_())
        {
            result = str.All(chr => chr == theOnlyCharToFind);
        }
        return result;
    }

    public static void CheckIsValidIndex_(this string str, int index)
    {
        if (!str.IsValidIndex_(index))
        {
            var subject = "string";

            int maxIndex = str.Length - 1;

            if (maxIndex < 0)
            {
                throw new UnexistingIndexBecauseEmptyException(index, subject);
            }

            int minIndex = 0;
            throw new OutOfRangeIntegerException(index, minIndex, maxIndex, $"{subject} Index");
        }
    }

    public static void CheckChunkExists_(this string str, int startIndex, int endIndex)
    {
        if (!str.ChunkExists_(startIndex, endIndex))
        {
            var subject = "string";
            if (str.IsEmpty_())
            {
                throw new UnexistingChunkBecauseEmptyException(startIndex, endIndex, subject);
            }

            int maxIndex = str.Length - 1;
            int minIndex = 0;
            throw new UnexistingChunkException(startIndex, endIndex, minIndex, maxIndex, subject);
        }
    }

    public static string GetChunk_(this string str, int startIndex, int endIndex)
    {
        str.CheckChunkExists_(startIndex, endIndex);
        var chunkLength = endIndex - startIndex + 1;
        var result = str.AsSpan(startIndex, chunkLength).ToString();
        return result;
    }

    public static string Repeat_(this string str, int nbRepeat)
    {
        if (nbRepeat < 0)
        {
            throw new MustBePositiveIntegerException(nbRepeat, nameof(nbRepeat));
        }
        char[] chars = str.ToArray();
        char[] resultChars = { };
        foreach (int _ in Enumerable.Range(1, nbRepeat))
        {
            resultChars = resultChars.Concat(chars).ToArray();
        }
        var string_ = new string(resultChars);
        return string_;
    }

    public static string EndsWith_(this string str, bool mustEndWith, string end)
    {
        var retour = str;
        var endsWith = str.EndsWith(end);

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

    public static string StartsWith_(this string str, bool mustStartWith, string start)
    {
        var retour = str;
        var startsWith = str.StartsWith(start);

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

    public static string Substring_(this string str, int startIndex, int substringLength)
    {
        string retour = string.Empty;

        if (startIndex < 0)
        {
            throw new MustBePositiveIntegerException(startIndex, "The index");
        }

        if (startIndex < str.Length)
        {
            if (startIndex + substringLength > str.Length)
            {
                substringLength = str.Length - startIndex;
            }
            retour = str.AsSpan(startIndex, substringLength).ToString();
        }

        return (retour);
    }
    public static string Substring_(this string str, int startIndex)
    {
        if (startIndex < 0)
        {
            throw new MustBePositiveIntegerException(startIndex, "The index");
        }
        var retour = (startIndex >= str.Length) ? string.Empty : str.AsSpan(startIndex).ToString();
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

    public static string CapitalizeFirst_(this string str)
    {
        if (str.IsEmpty_())
            return str;

        string result = str.Substring_(0, 1).ToUpper() + str.Substring_(1);
        return result;
    }

    public static string GetFromEnd_(this string str, int chunkLength)
    {
        //if (str.IsEmpty_())
        //{
        //    var subject = "String";
        //    throw new UnexistingChunkBecauseEmptyException(startIndex: null, endIndex: null, subject);
        //}

        if (chunkLength < 0)
        {
            throw new MustBePositiveIntegerException(chunkLength, "Chunk length");
        }

        if (chunkLength == 0 || str.IsEmpty_())
            return string.Empty;

        int startIndex = str.GetLastIndex_()!.Value - (chunkLength - 1);
        IEnumerable<char> chars = str.GetChunk_(startIndex);
        var result = chars.ToString_();
        return result;
    }

    public static string GetFromEndUntil_(this string str, Predicate<char> stopGettingCharsCondition)
    {
        List<char> endingChars = new();

        int strLength = str.Length;
        char element;
        for (int index = strLength - 1; index >= 0; index--)
        {
            element = str[index];

            if (stopGettingCharsCondition(element))
                break;

            endingChars.Add(element);
        }

        endingChars.Reverse();
        string result = endingChars.ToString_();
        return result;
    }

    public static List<string> ToList_(this string str)
    {
        var result = str.ToCharArray().Select(@char => $"{@char}").ToList();
        return result;
    }


    public static string ReplacePlaceHolders_(this string str, IDictionary<string, string> replacements)
    {
        string result = str;
        foreach (KeyValuePair<string, string> replacement in replacements)
        {
            result = result.Replace(replacement.Key, replacement.Value);
        }
        return result;
    }

    public static string Backslash_(this string str)
    {
        return str.Replace('/', '\\');
    }

    public static string Slash_(this string str)
    {
        return str.Replace('\\', '/');
    }
}