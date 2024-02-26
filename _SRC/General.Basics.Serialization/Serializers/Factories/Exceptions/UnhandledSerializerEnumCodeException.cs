namespace General.Basics.Serialization.Serializers.Factories;

public class UnhandledSerializerEnumCodeException : Exception
{
    public const string MESSAGE_FORMAT = "Code enum de Serializer actuellement non géré : '{0}'";
    public override string Message { get; }

    public UnhandledSerializerEnumCodeException(Serializer serializer) : base("")
    {
        Message = string.Format(MESSAGE_FORMAT, serializer);
    }
}