using Xunit;


namespace General.Basics.ErrorHandling.UnitTests;

public class StringOnlyContainsSpacesExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        var str = "  ";
        var ex = new StringOnlyContainsSpacesException(str);

        var expectedMessage = string.Format(StringOnlyContainsSpacesException.MESSAGE_FORMAT, str);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
