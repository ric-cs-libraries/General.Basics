using Xunit;


using General.Basics.Reflection.Extensions;



namespace General.Basics.Reflection.Extensions.UnitTests;


public class GenericParameterTypeViolatingSomeConstraintExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        string errorMessage = "...";

        var ex = new GenericParameterTypeViolatingSomeConstraintException(errorMessage);

        var expectedMessage = string.Format(GenericParameterTypeViolatingSomeConstraintException.MESSAGE_FORMAT, errorMessage);
        Assert.Equal(expectedMessage, ex.Message);
    }
}