using Xunit;

using General.Basics.Exceptions;


namespace General.Basics.Exceptions.UnitTests;


public class StringShouldNotBeEqualToAnyOfStringsExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        var str = "hello";
        string[] strings = { "HELLO", "hi", "you" };
        StringComparison comparisonMode = StringComparison.InvariantCultureIgnoreCase;

        var ex = new StringShouldNotBeEqualToAnyOfStringsException(comparisonMode, str, strings);

        var strings_ = $"'{string.Join("', '", strings)}'";
        var comparisonMode_ = Enum.GetName(typeof(StringComparison), comparisonMode);
        var expectedMessage = string.Format(StringShouldNotBeEqualToAnyOfStringsException.MESSAGE_FORMAT, comparisonMode_, str, strings_);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
