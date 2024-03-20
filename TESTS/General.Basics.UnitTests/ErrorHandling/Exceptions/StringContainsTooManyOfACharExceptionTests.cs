using Xunit;


using General.Basics.ErrorHandling;


namespace General.Basics.ErrorHandling.UnitTests;

public class StringContainsTooManyOfACharExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        var str = "AaAa";
        var nbTimes = 2;
        char theChar = 'a';
        var maxNbTimes = 2;
        var ex = new StringContainsTooManyOfACharException(str, nbTimes, theChar, maxNbTimes);

        var expectedMessage = string.Format(StringContainsTooManyOfACharException.MESSAGE_FORMAT, str, nbTimes, theChar, maxNbTimes);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
