using General.Basics.Reflection.Extensions;
using General.Basics.ReflectionExtended.Extensions;


namespace General.Basics.ReflectionExtended.DynamicCalls;

public class AppropriateAwaitableExpectedAsReturnedTypeException : Exception
{
    public const string MESSAGE_FORMAT = "The expected returned Type is : '{0}.Task{1}', not : '{2}'.";

    public override string Message { get; }

    //expectedReturnedValueType vaut null => type de retour attendu : Task.
    public AppropriateAwaitableExpectedAsReturnedTypeException(Type effectiveReturnedType, Type? expectedReturnedValueType)
    {
        Message = string.Format(MESSAGE_FORMAT,
            typeof(Task).Namespace,
            (expectedReturnedValueType is null) ? "" : $"<{expectedReturnedValueType.GetFullName_()}>",
            effectiveReturnedType.GetFullName_()
        );
    }
}
