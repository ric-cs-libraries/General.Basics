using Xunit;


using General.Basics.Reflection.DynamicCalls;



namespace General.Basics.Reflection.DynamicCalls.UnitTests;


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