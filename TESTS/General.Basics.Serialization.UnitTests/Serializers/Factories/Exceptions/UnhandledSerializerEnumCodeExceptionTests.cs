using Xunit;

using General.Basics.Serialization.Serializers.Factories;
using General.Basics.Serialization.Interfaces;


namespace General.Basics.Serialization.Serializers.Factories.UnitTests;


public class UnhandledSerializerEnumCodeExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        var unknownSerializerEnumCode = 10;
        var ex = new UnhandledSerializerEnumCodeException((Serializer)unknownSerializerEnumCode);

        var expectedMessage = $"Code enum de Serializer actuellement non géré : '{unknownSerializerEnumCode}'";
        Assert.Equal(expectedMessage, ex.Message);
    }
}
