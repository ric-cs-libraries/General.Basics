using General.Basics.Reflection.Extensions;


namespace General.Basics.Reflection.DynamicCalls;


public class AwaitableExpectedAsReturnedTypeException : Exception
{
    public const string MESSAGE_FORMAT = "For method '{0}' : expecting for an awaitable type as returned type.";

    public override string Message { get; }

    public AwaitableExpectedAsReturnedTypeException(string methodFullName) 
    {
        Message = string.Format(MESSAGE_FORMAT, methodFullName);
    }
}
