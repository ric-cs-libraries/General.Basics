namespace General.Basics.Others.Exceptions;

public class ValueShouldBeLowerOrEqualToException<T> : Exception
{
    public const string MESSAGE_FORMAT = "Invalid '{0}' = '{1}', it should be <= '{2}'.";

    public override string Message { get; }

    public ValueShouldBeLowerOrEqualToException(string subject, T actualValue, T maxValue)
    {
        Message = string.Format(MESSAGE_FORMAT, subject, actualValue, maxValue);
    }
}