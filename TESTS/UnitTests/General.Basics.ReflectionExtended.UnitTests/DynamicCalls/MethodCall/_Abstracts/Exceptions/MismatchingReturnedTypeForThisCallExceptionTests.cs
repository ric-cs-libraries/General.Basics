using Xunit;


using General.Basics.ReflectionExtended.DynamicCalls.Abstracts;

using General.Basics.ReflectionExtended.DynamicCalls;



namespace General.Basics.ReflectionExtended.DynamicCalls.UnitTests;

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