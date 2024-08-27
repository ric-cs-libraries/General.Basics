namespace General.Basics.ErrorHandling;

public class MustNotBeNullException : Exception
{
    public const string MESSAGE_FORMAT = "The variable '{0}' is null but must not !";

    public override string Message { get; }

    public MustNotBeNullException(string subject)
    {
        Message = string.Format(MESSAGE_FORMAT, subject);
    }
}