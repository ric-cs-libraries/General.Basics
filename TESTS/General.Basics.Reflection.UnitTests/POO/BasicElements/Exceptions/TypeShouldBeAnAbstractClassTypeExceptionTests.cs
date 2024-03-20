using Xunit;


using General.Basics.Reflection.POO;

namespace General.Basics.Reflection.POO.UnitTests;


public class TypeShouldBeAnAbstractClassTypeExceptionTests
{
    [Fact]
    public void Instanciation_WhenSubjectIsGiven_TheExceptionShouldContainTheCorrectMessage()
    {
        Type type = typeof(TypeShouldBeAnAbstractClassTypeExceptionTests);
        var ex = new TypeShouldBeAnAbstractClassTypeException(type.Name);

        var expectedMessage = string.Format(TypeShouldBeAnAbstractClassTypeException.MESSAGE_FORMAT, type.Name, TypeShouldBeAnAbstractClassTypeException.EXPECTED_TYPE_LABEL);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
