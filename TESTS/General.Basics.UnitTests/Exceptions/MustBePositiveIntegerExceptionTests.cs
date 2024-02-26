using Xunit;

using General.Basics.Exceptions;


namespace General.Basics.Exceptions.UnitTests;


public class MustBePositiveIntegerExceptionTests
{
    [Fact]
    public void Instanciation_WhenSubjectIsGiven_TheExceptionShouldContainTheCorrectMessage()
    {
        var subject = "The index";
        var negativeInt = -1;
        var ex = new MustBePositiveIntegerException(negativeInt, subject);

        var expectedMessage = string.Format(MustBePositiveIntegerException.MESSAGE_FORMAT, subject, negativeInt);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void Instanciation_WhenSubjectIsNotGiven_TheExceptionShouldContainTheCorrectMessage()
    {
        var subject = "Number";
        var negativeInt = -1;
        var ex = new MustBePositiveIntegerException(negativeInt);

        var expectedMessage = string.Format(MustBePositiveIntegerException.MESSAGE_FORMAT, subject, negativeInt);
        Assert.Equal(expectedMessage, ex.Message);
    }

}
