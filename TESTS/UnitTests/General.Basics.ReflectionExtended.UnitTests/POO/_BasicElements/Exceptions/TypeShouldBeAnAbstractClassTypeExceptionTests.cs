using General.Basics.Reflection.Extensions;
using General.Basics.ReflectionExtended.Extensions;
using Xunit;

namespace General.Basics.ReflectionExtended.POO.UnitTests;


public class TypeShouldBeAnAbstractClassTypeExceptionTests
{
    [Fact]
    public void Instanciation_WhenSubjectIsGiven_TheExceptionShouldContainTheCorrectMessage()
    {
        Type type = typeof(TypeShouldBeAnAbstractClassTypeExceptionTests);
        string typeFullName = type.GetFullName_();
        var ex = new TypeShouldBeAnAbstractClassTypeException(typeFullName);

        var expectedMessage = string.Format(TypeShouldBeAnAbstractClassTypeException.MESSAGE_FORMAT, typeFullName, TypeShouldBeAnAbstractClassTypeException.EXPECTED_TYPE_LABEL);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
