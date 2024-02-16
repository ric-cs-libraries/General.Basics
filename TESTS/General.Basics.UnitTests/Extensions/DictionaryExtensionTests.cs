using Xunit;

using General.Basics.Extensions;


namespace General.Basics.Extensions.UnitTests;


public class DictionaryExtensionTests
{
    private readonly Dictionary<string, int> dictionary;

    public DictionaryExtensionTests()
    {
        dictionary = new()
        {
            {"aa", 10 },
            {"Aa", 100 },
            {"Bb", 200 },
            {"C", 300 },
        };

    }


    [Fact]
    public void ToKeyValueString_DefaultBehavior_ShouldReturnTheRightString()
    {
        var result = dictionary.ToKeyValueString_<int>();
        var expected = $"aa={dictionary["aa"]};Aa={dictionary["Aa"]};Bb={dictionary["Bb"]};C={dictionary["C"]}";

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("/", "", "->")]
    [InlineData("*", "'", ":")]
    public void ToKeyValueString_WithParams_ShouldReturnTheRightString(string keyValueSeparator, string quoteValueSymbol, string keyValueEqualitySymbol)
    {
        var result = dictionary.ToKeyValueString_<int>(keyValueSeparator, quoteValueSymbol, keyValueEqualitySymbol);
        var expected = string.Join(keyValueSeparator, (new string[] {
            $"aa{keyValueEqualitySymbol}{quoteValueSymbol}{dictionary["aa"]}{quoteValueSymbol}",
            $"Aa{keyValueEqualitySymbol}{quoteValueSymbol}{dictionary["Aa"]}{quoteValueSymbol}",
            $"Bb{keyValueEqualitySymbol}{quoteValueSymbol}{dictionary["Bb"]}{quoteValueSymbol}",
            $"C{keyValueEqualitySymbol}{quoteValueSymbol}{dictionary["C"]}{quoteValueSymbol}",
        }));

        Assert.Equal(expected, result);
    }
}
