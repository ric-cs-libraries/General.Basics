namespace General.Basics.ReflectionExtended.DynamicCalls;

public class CheckIfIsNonGenericTypeException : Exception
{
    public const string MESSAGE_FORMAT = "This instanciator is only suitable for NON generic types. Please, check if the following type is a NON generic one : '{0}'.";

    public override string Message { get; }

    public CheckIfIsNonGenericTypeException(string typeFullName)
    {
        Message = string.Format(MESSAGE_FORMAT, typeFullName);
    }
}
