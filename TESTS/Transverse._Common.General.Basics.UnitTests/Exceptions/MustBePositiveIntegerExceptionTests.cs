using Xunit;

using Transverse._Common.General.Basics.Exceptions;


namespace Transverse._Common.General.Basics.Exceptions.UnitTests;


public class MustBePositiveIntegerExceptionTests
{
    [Fact]
    public void Instanciation_WhenSubjectIsGiven_TheExceptionShouldContainTheCorrectMessage()
    {
        var subject = "The index";
        var negativeInt = -1;
        var ex = new MustBePositiveIntegerException(negativeInt, subject);

        var expectedMessage = $"{subject} must be a >=0 integer : '{negativeInt}' unauthorized.";
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void Instanciation_WhenSubjectIsNotGiven_TheExceptionShouldContainTheCorrectMessage()
    {
        var subject = "Number";
        var negativeInt = -1;
        var ex = new MustBePositiveIntegerException(negativeInt);

        var expectedMessage = $"{subject} must be a >=0 integer : '{negativeInt}' unauthorized.";
        Assert.Equal(expectedMessage, ex.Message);
    }

}
