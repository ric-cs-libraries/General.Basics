using Xunit;


using General.Basics.Reflection.POO;

namespace General.Basics.Reflection.POO.UnitTests;


public class TypeShouldBeAnInterfaceTypeExceptionTests
{
    [Fact]
    public void Instanciation_WhenSubjectIsGiven_TheExceptionShouldContainTheCorrectMessage()
    {
        Type type = typeof(TypeShouldBeAnAbstractClassTypeExceptionTests);
        var ex = new TypeShouldBeAnInterfaceTypeException(type.Name);

        var expectedMessage = string.Format(TypeShouldBeAnInterfaceTypeException.MESSAGE_FORMAT, type.Name, TypeShouldBeAnInterfaceTypeException.EXPECTED_TYPE_LABEL);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
