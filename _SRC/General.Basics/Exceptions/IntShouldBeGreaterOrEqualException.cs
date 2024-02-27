namespace General.Basics.Exceptions;

public class IntShouldBeGreaterOrEqualException : Exception
{
    public const string MESSAGE_FORMAT = "Invalid {0} = {1}, this int should be >= {2}.";

    public override string Message { get; }

    public IntShouldBeGreaterOrEqualException(string subject, int actualValue, int minimalValue)
    {
        Message = string.Format(MESSAGE_FORMAT, subject, actualValue, minimalValue);
    }
}