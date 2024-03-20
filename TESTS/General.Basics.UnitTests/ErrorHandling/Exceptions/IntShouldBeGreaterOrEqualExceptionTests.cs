using Xunit;


using General.Basics.ErrorHandling;


namespace General.Basics.ErrorHandling.UnitTests;

public class IntShouldBeGreaterOrEqualExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        var str = "index";
        var actualValue = 4;
        var minimalValue = 5;
        var ex = new IntShouldBeGreaterOrEqualException(str, actualValue, minimalValue);

        var expectedMessage = string.Format(IntShouldBeGreaterOrEqualException.MESSAGE_FORMAT, str, actualValue, minimalValue);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
