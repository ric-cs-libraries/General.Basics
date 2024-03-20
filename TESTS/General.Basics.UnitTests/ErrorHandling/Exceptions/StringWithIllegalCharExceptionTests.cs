using Xunit;


using General.Basics.ErrorHandling;


namespace General.Basics.ErrorHandling.UnitTests;

public class StringWithIllegalCharExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        var str = "CaT";
        char illegalChar = 'a';
        var ex = new StringWithIllegalCharException(str, illegalChar);

        var expectedMessage = string.Format(StringWithIllegalCharException.MESSAGE_FORMAT, str, illegalChar);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
