using Xunit;


using General.Basics.ErrorHandling;


namespace General.Basics.ErrorHandling.UnitTests;

public class LongShouldBeGreaterOrEqualExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        var str = "myLongInt";
        long actualValue = 4L;
        long minimalValue = 5L;
        var ex = new LongShouldBeGreaterOrEqualException(str, actualValue, minimalValue);

        var expectedMessage = string.Format(LongShouldBeGreaterOrEqualException.MESSAGE_FORMAT, str, actualValue, minimalValue);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
