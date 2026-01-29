namespace General.Basics.Extensions.ErrorHandling;

public class CannotDowncastException : Exception
{
    public const string MESSAGE_FORMAT = "Cannot convert an instance of '{0}' to an instance of '{1}'  ({2}).";

    public override string Message { get; }

    public CannotDowncastException(string fromTypeName, string toTypeName, string subject)
    {
        Message = string.Format(MESSAGE_FORMAT, fromTypeName, toTypeName, subject);
    }
}