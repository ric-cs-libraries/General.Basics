using Xunit;


using General.Basics.Reflection.DynamicCalls.Abstracts;

using General.Basics.Reflection.DynamicCalls;



namespace General.Basics.Reflection.DynamicCalls.UnitTests;

public class MismatchingReturnedTypeForThisCallExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        Type expectedReturnedValueType = typeof(bool);
        Type effectiveReturnedValueType = typeof(int);

        var ex = new MismatchingReturnedValueTypeForThisCallException(expectedReturnedValueType, effectiveReturnedValueType);

        var expectedMessage = string.Format(MismatchingReturnedValueTypeForThisCallException.MESSAGE_FORMAT, expectedReturnedValueType, effectiveReturnedValueType);
        Assert.Equal(expectedMessage, ex.Message);
    }
}