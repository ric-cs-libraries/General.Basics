using Xunit;


using General.Basics.Reflection.POO;

namespace General.Basics.Reflection.POO.UnitTests;


public class TypeShouldBeAClassTypeExceptionTests
{
    [Fact]
    public void Instanciation_WhenSubjectIsGiven_TheExceptionShouldContainTheCorrectMessage()
    {
        Type type = typeof(int);
        var ex = new TypeShouldBeAClassTypeException(type.Name);

        var expectedMessage = string.Format(TypeShouldBeAClassTypeException.MESSAGE_FORMAT, type.Name, TypeShouldBeAClassTypeException.EXPECTED_TYPE_LABEL);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
