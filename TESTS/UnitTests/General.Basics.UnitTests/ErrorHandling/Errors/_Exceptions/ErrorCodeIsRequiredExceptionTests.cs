using Xunit;


using General.Basics.ErrorHandling;


namespace General.Basics.ErrorHandling.UnitTests;

public class ErrorCodeIsRequiredExceptionTests
{

    [Fact]
    public void Instanciation___TheExceptionShouldContainTheCorrectMessage()
    {
        //--- Arrange ---
        var ex = new ErrorCodeIsRequiredException();


        //--- Act ---
        var result = ex.Message;

        //--- Assert ---
        var expectedMessage = ErrorCodeIsRequiredException.MESSAGE;
        Assert.Equal(expectedMessage, result);
    }
}