using General.Basics.Reflection.Extensions;
using General.Basics.ReflectionExtended.Extensions;
using Xunit;

namespace General.Basics.ReflectionExtended.POO.UnitTests;


public class TypeShouldBeAClassTypeExceptionTests
{
    [Fact]
    public void Instanciation_WhenSubjectIsGiven_TheExceptionShouldContainTheCorrectMessage()
    {
        Type type = typeof(int);
        string typeFullName = type.GetFullName_();
        var ex = new TypeShouldBeAClassTypeException(typeFullName);

        var expectedMessage = string.Format(TypeShouldBeAClassTypeException.MESSAGE_FORMAT, typeFullName, TypeShouldBeAClassTypeException.EXPECTED_TYPE_LABEL);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
