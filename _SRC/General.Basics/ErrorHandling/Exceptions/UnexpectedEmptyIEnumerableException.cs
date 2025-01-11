

namespace General.Basics.ErrorHandling;

public class UnexpectedEmptyIEnumerableException : Exception
{
    public const string MESSAGE_FORMAT = "IEnumerable '{0}' must not be empty.";

    public override string Message { get; }

    public UnexpectedEmptyIEnumerableException(string enumerableName)
    {
        Message = string.Format(MESSAGE_FORMAT, enumerableName);
    }
}