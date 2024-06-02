namespace General.Basics.ErrorHandling;

public class CannotSearchElementBecauseEmptyException : Exception
{
    public const string MESSAGE_FORMAT = "Cannot consider searching any element in '{0}' because it is empty !";
    public override string Message { get; }

    public CannotSearchElementBecauseEmptyException(string subject)
    {
        Message = string.Format(MESSAGE_FORMAT, subject);
    }
}