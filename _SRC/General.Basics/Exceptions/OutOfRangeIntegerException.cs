namespace General.Basics.Exceptions;

public class OutOfRangeIntegerException : Exception
{
    public const string MESSAGE_FORMAT = "Invalid {0} : '{1}', possible range : [{2},{3}].";

    public override string Message { get; }

    public OutOfRangeIntegerException(int invalidIndex, int minIndex, int maxIndex, string? subject = null)
    {
        subject ??= "Number";
        Message = string.Format(MESSAGE_FORMAT, subject, invalidIndex, minIndex, maxIndex);
    }
}