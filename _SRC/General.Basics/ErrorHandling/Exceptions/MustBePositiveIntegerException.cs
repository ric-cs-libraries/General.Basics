namespace General.Basics.ErrorHandling;

public class MustBePositiveIntegerException : Exception
{
    public const string MESSAGE_FORMAT = "{0} must be a >=0 integer : '{1}' unauthorized.";

    public override string Message { get; }

    public MustBePositiveIntegerException(int negativeInt, string? subject = null)
    {
        subject ??= "Number";
        Message = string.Format(MESSAGE_FORMAT, subject, negativeInt);
    }
}