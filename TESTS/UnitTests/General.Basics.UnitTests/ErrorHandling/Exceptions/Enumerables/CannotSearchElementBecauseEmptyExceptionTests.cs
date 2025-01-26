using Xunit;


using General.Basics.ErrorHandling;


namespace General.Basics.ErrorHandling.UnitTests;

public class CannotSearchElementBecauseEmptyExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        var subject = "ListX";

        var ex = new CannotSearchElementBecauseEmptyException(subject);

        var expectedMessage = string.Format(CannotSearchElementBecauseEmptyException.MESSAGE_FORMAT, subject);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
