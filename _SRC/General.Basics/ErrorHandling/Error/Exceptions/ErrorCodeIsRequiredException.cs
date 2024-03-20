
namespace General.Basics.ErrorHandling;

public class ErrorCodeIsRequiredException : Exception
{
    public const string MESSAGE = "Error Code is required";
    public override string Message { get; }

    public ErrorCodeIsRequiredException() : base("")
    {
        Message = MESSAGE;
    }
}