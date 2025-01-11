using Xunit;


using General.Basics.ErrorHandling;


namespace General.Basics.ErrorHandling.UnitTests;

public class MustNotBeNullExceptionTests
{
    [Fact]
    public void Instanciation_WhenSubjectIsGiven_TheExceptionShouldContainTheCorrectMessage()
    {
        var subject = "myVar";
        var ex = new MustNotBeNullException(subject);

        var expectedMessage = string.Format(MustNotBeNullException.MESSAGE_FORMAT, subject);
        Assert.Equal(expectedMessage, ex.Message);
    }

}
