using General.Basics.Converters.StringConverters.Abstracts;
using General.Basics.Converters.Strings.Interfaces;
using Xunit;

namespace General.Basics.UnitTests.Converters.Strings.Abstracts;

public class StringConverterBaseTests
{
    [Fact]
    public void Convert__ShouldApplyAllConvertersConversion()
    {
        //--- Arrange ---
        var stringConverter1 = new Fixtures.StringConverter1();
        var stringConverter2 = new Fixtures.StringConverter2(stringConverter1);
        var stringToConvert = "Hello";

        //--- Act ---
        var result = stringConverter2.Convert(stringToConvert);

        //--- Assert ---
        var expected = $"**>'{stringToConvert}'<**";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Convert__ShouldApplyAllConvertersConversion_2()
    {
        //--- Arrange ---
        var stringConverter1 = new Fixtures.StringConverter1();
        var stringConverter2 = new Fixtures.StringConverter2(stringConverter1);
        var stringConverter3 = new Fixtures.StringConverter1(stringConverter2);
        var stringToConvert = "Hello";

        //--- Act ---
        var result = stringConverter3.Convert(stringToConvert);

        //--- Assert ---
        var expected = $"'**>'{stringToConvert}'<**'";
        Assert.Equal(expected, result);
    }

    //=============================================
    static class Fixtures
    {
        public class StringConverter1 : StringConverterBase
        {
            public StringConverter1(IStringConverter? previousStringConverter = null) : base(previousStringConverter) { }
            protected override string SpecificConvert(string str)
            {
                return $"'{str}'";
            }
        }
        public class StringConverter2 : StringConverterBase
        {
            public StringConverter2(IStringConverter? previousStringConverter = null) : base(previousStringConverter) { }
            protected override string SpecificConvert(string str)
            {
                return $"**>{str}<**";
            }
        }

    }
}


