namespace General.Basics.Serialization.Serializers.Factories;

public class UnhandledSerializerEnumCodeException : Exception
{
    public override string Message { get; }

    public UnhandledSerializerEnumCodeException(Serializer serializer) : base("")
    {
        Message = $"Code enum de Serializer actuellement non géré : '{serializer}'";
    }
}