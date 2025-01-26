using Xunit;


namespace General.Basics.ErrorHandling.UnitTests;

public class UnavailableResultValueExceptionTests
{

    [Fact]
    public void Instanciation___TheExceptionShouldContainTheCorrectMessage()
    {
        //--- Arrange ---
        Error error = new Error(code: "20", debugMessageTemplate: "A");
        Result result_ = Result.NotOk(error);
        var ex = new UnavailableResultValueException(result_);


        //--- Act ---
        var result = ex.Message;

        //--- Assert ---
        var expectedMessage = string.Format(UnavailableResultValueException.MESSAGE, result_.ToString());
        Assert.Equal(expectedMessage, result);
    }
}