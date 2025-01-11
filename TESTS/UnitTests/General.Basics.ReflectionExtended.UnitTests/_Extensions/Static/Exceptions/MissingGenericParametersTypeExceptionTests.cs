using Xunit;

using General.Basics.ReflectionExtended.Extensions;


namespace General.Basics.ReflectionExtended.Extensions.UnitTests;


public class MissingGenericParametersTypeExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        var methodName = $"{nameof(Type_)}.{nameof(Type_.GetGenericTypeFromNamesInfos)}";
        var genericClassName = "MyGenericInterface";
        var ex = new MissingGenericParametersTypeException(methodName, genericClassName);

        var expectedMessage = string.Format(MissingGenericParametersTypeException.MESSAGE_FORMAT, methodName, genericClassName);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
