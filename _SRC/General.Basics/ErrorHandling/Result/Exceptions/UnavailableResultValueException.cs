
namespace General.Basics.ErrorHandling;

public class UnavailableResultValueException : Exception
{
    public const string MESSAGE = "Due to the operation failure, the Result value is not available. ({0})";
    public override string Message { get; }

    public UnavailableResultValueException(Result result) : base("")
    {
        Message = string.Format(MESSAGE, result.ToString());
    }
}