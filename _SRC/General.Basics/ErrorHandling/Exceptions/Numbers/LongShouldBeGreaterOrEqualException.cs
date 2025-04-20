namespace General.Basics.ErrorHandling;

public class LongShouldBeGreaterOrEqualException : Exception
{
    public const string MESSAGE_FORMAT = "Invalid {0} = {1}, this long int should be >= {2}.";

    public override string Message { get; }

    public LongShouldBeGreaterOrEqualException(string subject, long actualValue, long minimalValue)
    {
        Message = string.Format(MESSAGE_FORMAT, subject, actualValue, minimalValue);
    }
}