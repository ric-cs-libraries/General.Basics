using Xunit;


using General.Basics.ErrorHandling;


namespace General.Basics.ErrorHandling.UnitTests;

public class UnexistingIndexBecauseEmptyExceptionTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage(int index)
    {
        var subject = "List";

        var ex = new UnexistingIndexBecauseEmptyException(index, subject);

        var expectedMessage = string.Format(UnexistingIndexBecauseEmptyException.MESSAGE_FORMAT, subject, index);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
