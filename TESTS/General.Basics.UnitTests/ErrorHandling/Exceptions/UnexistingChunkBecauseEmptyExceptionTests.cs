using Xunit;


using General.Basics.ErrorHandling;


namespace General.Basics.ErrorHandling.UnitTests;

public class UnexistingChunkBecauseEmptyExceptionTests
{
    [Theory]
    [InlineData(0,0)]
    [InlineData(0, 1)]
    [InlineData(2, 3)]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage(int startIndex, int endIndex)
    {
        var subject = "List";

        var ex = new UnexistingChunkBecauseEmptyException(startIndex, endIndex, subject);

        var expectedMessage = string.Format(UnexistingChunkBecauseEmptyException.MESSAGE_FORMAT, subject, startIndex, endIndex);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
