using Xunit;


using General.Basics.Others.Exceptions;


namespace General.Basics.Others.Exceptions.UnitTests;

public class ValueShouldNotBeGreaterThanExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        var subject = "Index";
        var actualValue = 4;
        var maxValue = 3;
        var ex = new ValueShouldBeLowerOrEqualToException<int>(subject, actualValue, maxValue);

        var expectedMessage = string.Format(ValueShouldBeLowerOrEqualToException<int>.MESSAGE_FORMAT, subject, actualValue, maxValue);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
