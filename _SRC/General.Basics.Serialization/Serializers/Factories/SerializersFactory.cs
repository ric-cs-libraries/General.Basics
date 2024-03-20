using General.Basics.Serialization.Serializers.Interfaces;

namespace General.Basics.Serialization.Serializers.Factories;

public enum Serializer
{
    Native = 0,
    NewtonSoft = 1,
}

public class SerializersFactory
{
    private static ISerializer nativeSerializer = null!;
    private static ISerializer newtonSoftSerializer = null!;

    private static SerializersFactory factory = null!;

    public Serializer Serializer { get; private set; } //enum

    private SerializersFactory(Serializer serializer)
    {
        Serializer = serializer;
    }

    public static SerializersFactory Create(Serializer serializer = Serializer.NewtonSoft)
    {
        if (factory is null)
        {
            factory = new SerializersFactory(serializer);
        } 
        else
        {
            factory.Serializer = serializer;
        }
        return factory;
    }

    public ISerializer Get()
    {
        ISerializer result = Serializer switch
        {
            Serializer.Native => GetNativeSerializer(),
            Serializer.NewtonSoft => GetNewtonSoftSerializer(),
            _ => throw new UnhandledSerializerEnumCodeException(Serializer)
        };
        return result;
    }
    private static ISerializer GetNewtonSoftSerializer()
    {
        newtonSoftSerializer ??= NewtonSoftSerializer.Create();
        return newtonSoftSerializer;
    }
    private static ISerializer GetNativeSerializer()
    {
        nativeSerializer ??= NativeSerializer.Create();
        return nativeSerializer;

    }
}
