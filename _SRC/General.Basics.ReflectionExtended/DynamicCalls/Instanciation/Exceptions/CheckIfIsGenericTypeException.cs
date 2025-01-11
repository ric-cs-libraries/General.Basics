namespace General.Basics.ReflectionExtended.DynamicCalls;

public class CheckIfIsGenericTypeException : Exception
{
    public const string MESSAGE_FORMAT = "This instanciator is only suitable for Generic types. Please, check if the following type is a Generic one : '{0}'.";

    public override string Message { get; }

    public CheckIfIsGenericTypeException(string typeFullName)
    {
        Message = string.Format(MESSAGE_FORMAT, typeFullName);
    }
}
