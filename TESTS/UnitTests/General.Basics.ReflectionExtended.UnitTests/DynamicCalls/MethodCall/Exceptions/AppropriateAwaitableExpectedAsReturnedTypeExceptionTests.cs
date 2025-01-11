using General.Basics.Reflection.Extensions;
using General.Basics.ReflectionExtended.Extensions;
using Xunit;



namespace General.Basics.ReflectionExtended.DynamicCalls.UnitTests;

public class AppropriateAwaitableExpectedAsReturnedTypeExceptionTests
{
    [Fact]
    public void Instanciation_WhenExistsAReturnedValueType_TheExceptionShouldContainTheCorrectMessage()
    {
        Type? expectedReturnedValueType = typeof(bool); //Pour Task<bool>
        Type effectiveReturnedType = typeof(Task<string>);

        var ex = new AppropriateAwaitableExpectedAsReturnedTypeException(effectiveReturnedType, expectedReturnedValueType);

        var expectedMessage = string.Format(AppropriateAwaitableExpectedAsReturnedTypeException.MESSAGE_FORMAT,
            typeof(Task).Namespace,
            $"<{expectedReturnedValueType.GetFullName_()}>",
            effectiveReturnedType.GetFullName_()
        );
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void Instanciation_WhenDoesntExistAReturnedValueType_TheExceptionShouldContainTheCorrectMessage()
    {
        Type? expectedReturnedValueType = null; //Pour Task
        Type effectiveReturnedType = typeof(void);

        var ex = new AppropriateAwaitableExpectedAsReturnedTypeException(effectiveReturnedType, expectedReturnedValueType);

        var expectedMessage = string.Format(AppropriateAwaitableExpectedAsReturnedTypeException.MESSAGE_FORMAT,
            typeof(Task).Namespace,
            string.Empty,
            effectiveReturnedType.GetFullName_()
        );
        Assert.Equal(expectedMessage, ex.Message);
    }
}