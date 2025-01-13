using Xunit;

using General.Basics.Serialization.Serializers.Interfaces;


using General.Basics.Serialization.Serializers.Factories;


namespace General.Basics.Serialization.Serializers.Factories.UnitTests;

public class SerializersFactoryTests
{
    [Fact]
    public void Create__ShouldReturnANewInstanceOfSerializersFactory()
    {
        SerializersFactory factory = SerializersFactory.Create();

        Assert.IsType<SerializersFactory>(factory);
    }

    [Fact]
    public void Create__ShoulAlwaysReturnASingleton()
    {
        SerializersFactory factory1 = SerializersFactory.Create(Serializer.NewtonSoft);
        SerializersFactory factory2 = SerializersFactory.Create(Serializer.Native);
        SerializersFactory factory3 = SerializersFactory.Create();

        Assert.Equal(factory1, factory2);
        Assert.Equal(factory2, factory3);
    }

    [Fact]
    public void Get_WhenFactoryWasInstanciatedWithNoSerializerEnumCode_ShouldReturnAnInstanceOfNewtonSoftSerializer()
    {
        SerializersFactory factory1 = SerializersFactory.Create(Serializer.Native);
        SerializersFactory factory2 = SerializersFactory.Create();

        ISerializer serializer = factory2.GetSingleton();

        Assert.IsType<NewtonSoftSerializer>(serializer);
    }

    [Fact]
    public void Get_WhenFactoryWasInstanciatedWithNewtonSoftSerializerEnumCode_ShouldReturnAnInstanceOfNewtonSoftSerializer()
    {
        SerializersFactory factory1 = SerializersFactory.Create(Serializer.Native);
        SerializersFactory factory2 = SerializersFactory.Create(Serializer.NewtonSoft);

        ISerializer serializer = factory2.GetSingleton();

        Assert.IsType<NewtonSoftSerializer>(serializer);
    }

    [Fact]
    public void Get_WhenFactoryWasInstanciatedWithNativeSerializerEnumCode_ShouldReturnAnInstanceOfNativeSerializer()
    {
        SerializersFactory factory1 = SerializersFactory.Create(Serializer.NewtonSoft);
        SerializersFactory factory2 = SerializersFactory.Create(Serializer.Native);

        ISerializer serializer = factory2.GetSingleton();

        Assert.IsType<NativeSerializer>(serializer);
    }

    [Fact]
    public void Get___ShouldAlwaysReturnASingleton()
    {
        ISerializer native1 = SerializersFactory.Create(Serializer.NewtonSoft).GetSingleton();
        ISerializer newtonS1 = SerializersFactory.Create(Serializer.Native).GetSingleton();
        ISerializer native2 = SerializersFactory.Create(Serializer.NewtonSoft).GetSingleton();
        ISerializer newtonS2 = SerializersFactory.Create(Serializer.Native).GetSingleton();

        Assert.Equal(native1, native2);
        Assert.Equal(newtonS1, newtonS2);
    }

    [Fact]
    public void Get_WhenFactoryWasInstanciatedWithAnUnhandledSerializerEnumCode_ShouldThrowAnUnhandledSerializerEnumCodeException()
    {
        int unknownSerializerEnumCode = 10;
        var factory  = SerializersFactory.Create((Serializer)unknownSerializerEnumCode);

        var ex = Assert.Throws<UnhandledSerializerEnumCodeException>(() => factory.GetSingleton());
    }
}