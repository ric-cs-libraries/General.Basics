using Xunit;


using General.Basics.Reflection.DynamicCalls.Abstracts;



namespace General.Basics.Reflection.DynamicCalls.UnitTests;


public class AnAbstractTypeCannotBeInstanciatedExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        string typeFullName = "A.B.C.IMyInterface";

        var ex = new AnAbstractTypeCannotBeInstanciatedException(typeFullName);

        var expectedMessage = string.Format(AnAbstractTypeCannotBeInstanciatedException.MESSAGE_FORMAT, typeFullName);
        Assert.Equal(expectedMessage, ex.Message);
    }
}