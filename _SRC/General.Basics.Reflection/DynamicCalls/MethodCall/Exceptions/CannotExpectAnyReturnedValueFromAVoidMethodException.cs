using General.Basics.Reflection.Extensions;


namespace General.Basics.Reflection.DynamicCalls;


public class CannotExpectAnyReturnedValueFromAVoidMethodException : Exception
{
    public const string MESSAGE_FORMAT = "The method '{0}' has void as return type, so you can't expect any returned value from it !";

    public override string Message { get; }

    public CannotExpectAnyReturnedValueFromAVoidMethodException(string methodFullName)
    {
        Message = string.Format(MESSAGE_FORMAT, methodFullName);
    }
}
