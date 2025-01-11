using Xunit;


using General.Basics.ReflectionExtended.DynamicCalls;



namespace General.Basics.ReflectionExtended.DynamicCalls.UnitTests;


public class CheckIfIsNonGenericTypeExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        string typeFullName = "A.B.C.MyGenericClass";

        var ex = new CheckIfIsNonGenericTypeException(typeFullName);

        var expectedMessage = string.Format(CheckIfIsNonGenericTypeException.MESSAGE_FORMAT, typeFullName);
        Assert.Equal(expectedMessage, ex.Message);
    }
}