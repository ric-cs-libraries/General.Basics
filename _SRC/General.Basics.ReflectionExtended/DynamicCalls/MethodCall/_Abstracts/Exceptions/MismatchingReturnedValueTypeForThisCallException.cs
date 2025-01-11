using General.Basics.Reflection.Extensions;
using General.Basics.ReflectionExtended.Extensions;


namespace General.Basics.ReflectionExtended.DynamicCalls.Abstracts;

public class MismatchingReturnedValueTypeForThisCallException : Exception
{
    public const string MESSAGE_FORMAT = "The expected returned Value Type '{0}' doesn't match the effective returned Value Type '{1}', but it should !";

    public override string Message { get; }

    public MismatchingReturnedValueTypeForThisCallException(Type expectedReturnedValueType, Type effectiveReturnedValueType)
    {
        Message = string.Format(MESSAGE_FORMAT, expectedReturnedValueType.GetFullName_(), effectiveReturnedValueType.GetFullName_());
    }
}
