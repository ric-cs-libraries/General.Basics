using Xunit;

using General.Basics.Exceptions;


namespace General.Basics.Exceptions.UnitTests;


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
