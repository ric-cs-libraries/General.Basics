using General.Basics.Reflection.Extensions;
using General.Basics.ReflectionExtended.Extensions;
using Xunit;

namespace General.Basics.ReflectionExtended.POO.UnitTests;


public class TypeShouldBeAnInterfaceTypeExceptionTests
{
    [Fact]
    public void Instanciation_WhenSubjectIsGiven_TheExceptionShouldContainTheCorrectMessage()
    {
        Type type = typeof(TypeShouldBeAnAbstractClassTypeExceptionTests);
        string typeFullName = type.GetFullName_();
        var ex = new TypeShouldBeAnInterfaceTypeException(typeFullName);

        var expectedMessage = string.Format(TypeShouldBeAnInterfaceTypeException.MESSAGE_FORMAT, typeFullName, TypeShouldBeAnInterfaceTypeException.EXPECTED_TYPE_LABEL);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
