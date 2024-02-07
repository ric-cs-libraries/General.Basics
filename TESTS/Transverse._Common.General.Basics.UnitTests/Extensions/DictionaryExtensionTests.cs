using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//using Moq;

using Transverse._Common.General.Basics.Extensions;


namespace Transverse._Common.General.Basics.UnitTests.Extensions;

[TestClass]
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


    [TestMethod]
    public void ToKeyValueString_DefaultBehavior_ShouldReturnTheRightString()
    {
        var result = dictionary.ToKeyValueString_<int>();
        var expected = $"aa={dictionary["aa"]};Aa={dictionary["Aa"]};Bb={dictionary["Bb"]};C={dictionary["C"]}";

        expected.Should().Be(result);
    }

    [TestMethod]
    [DataRow("/", "", "->")]
    [DataRow("*", "'", ":")]
    public void ToKeyValueString_WithParams_ShouldReturnTheRightString(string keyValueSeparator, string quoteValueSymbol, string keyValueEqualitySymbol)
    {
        var result = dictionary.ToKeyValueString_<int>(keyValueSeparator, quoteValueSymbol, keyValueEqualitySymbol);
        var expected = string.Join(keyValueSeparator, (new string[] {
            $"aa{keyValueEqualitySymbol}{quoteValueSymbol}{dictionary["aa"]}{quoteValueSymbol}",
            $"Aa{keyValueEqualitySymbol}{quoteValueSymbol}{dictionary["Aa"]}{quoteValueSymbol}",
            $"Bb{keyValueEqualitySymbol}{quoteValueSymbol}{dictionary["Bb"]}{quoteValueSymbol}",
            $"C{keyValueEqualitySymbol}{quoteValueSymbol}{dictionary["C"]}{quoteValueSymbol}",
        }));

        expected.Should().Be(result);
    }
}
