using Xunit;


using General.Basics.Reflection.DynamicCalls;



namespace General.Basics.Reflection.DynamicCalls.UnitTests;


public class CannotExpectAnyReturnedValueFromAVoidMethodExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        string methodFullName = "System.String MyClassNameSpace.MyClass.MyMethod(System.Int32, System.DateTime)";

        var ex = new CannotExpectAnyReturnedValueFromAVoidMethodException(methodFullName);

        var expectedMessage = string.Format(CannotExpectAnyReturnedValueFromAVoidMethodException.MESSAGE_FORMAT, methodFullName);
        Assert.Equal(expectedMessage, ex.Message);
    }
}