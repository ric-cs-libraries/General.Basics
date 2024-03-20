using Xunit;


using General.Basics.Serialization.Serializers.Factories;


namespace General.Basics.Serialization.Serializers.Factories.UnitTests;


public class UnhandledSerializerEnumCodeExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        var unknownSerializerEnumCode = 10;
        var ex = new UnhandledSerializerEnumCodeException((Serializer)unknownSerializerEnumCode);

        var expectedMessage = string.Format(UnhandledSerializerEnumCodeException.MESSAGE_FORMAT, unknownSerializerEnumCode);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
