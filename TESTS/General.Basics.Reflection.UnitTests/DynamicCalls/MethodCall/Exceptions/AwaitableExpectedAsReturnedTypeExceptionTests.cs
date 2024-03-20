using Xunit;



using General.Basics.Reflection.DynamicCalls;



namespace General.Basics.Reflection.DynamicCalls.UnitTests;


public class AwaitableExpectedAsReturnedTypeExceptionTests
{
    [Fact]
    public void Instanciation_WhenExistsAReturnedValueType_TheExceptionShouldContainTheCorrectMessage()
    {
        string methodFullName = "System.String MyClassNameSpace.MyClass.MyMethod()";

        var ex = new AwaitableExpectedAsReturnedTypeException(methodFullName);

        var expectedMessage = string.Format(AwaitableExpectedAsReturnedTypeException.MESSAGE_FORMAT, methodFullName);
        Assert.Equal(expectedMessage, ex.Message);
    }
}