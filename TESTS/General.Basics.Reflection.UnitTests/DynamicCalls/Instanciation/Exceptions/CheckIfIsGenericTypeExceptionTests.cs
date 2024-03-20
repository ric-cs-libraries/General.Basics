using Xunit;


using General.Basics.Reflection.DynamicCalls;



namespace General.Basics.Reflection.DynamicCalls.UnitTests;


public class CheckIfIsGenericTypeExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        string typeFullName = "A.B.C.MyClass";

        var ex = new CheckIfIsGenericTypeException(typeFullName);

        var expectedMessage = string.Format(CheckIfIsGenericTypeException.MESSAGE_FORMAT, typeFullName);
        Assert.Equal(expectedMessage, ex.Message);
    }
}