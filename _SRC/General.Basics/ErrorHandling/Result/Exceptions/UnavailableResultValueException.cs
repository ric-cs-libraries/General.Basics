
namespace General.Basics.ErrorHandling;

public class UnavailableResultValueException : Exception
{
    public const string MESSAGE = "Due to the operation failure, the Result value is not available.";
    public override string Message { get; }

    public UnavailableResultValueException() : base("")
    {
        Message = MESSAGE;
    }
}