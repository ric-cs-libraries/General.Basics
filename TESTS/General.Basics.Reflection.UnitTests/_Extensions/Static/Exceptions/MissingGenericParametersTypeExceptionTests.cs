using Xunit;

using General.Basics.Reflection.Extensions;


namespace General.Basics.Reflection.Extensions.UnitTests;


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
