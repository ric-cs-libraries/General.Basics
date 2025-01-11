namespace General.Basics.ErrorHandling;

public class UnfoundKeyException : Exception
{
    public const string MESSAGE_FORMAT = "Key '{0}' not found in '{1}'.";

    public override string Message { get; }

    public UnfoundKeyException(object? key, string subject)
    {
        Message = string.Format(MESSAGE_FORMAT, key, subject);
    }
}