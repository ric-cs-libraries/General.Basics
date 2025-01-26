namespace General.Basics.Extensions.ErrorHandling;

public class CannotDowncastException : Exception
{
    public const string MESSAGE_FORMAT = "{0} : cannot convert an instance of '{1}' to an instance of '{2}'.";

    public override string Message { get; }

    public CannotDowncastException(string subject, string fromTypeName, string toTypeName)
    {
        Message = string.Format(MESSAGE_FORMAT, subject, fromTypeName, toTypeName);
    }
}