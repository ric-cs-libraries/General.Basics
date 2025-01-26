namespace General.Basics.ErrorHandling;

public class UnexistingIndexBecauseEmptyException : Exception
{
    public const string MESSAGE_FORMAT = "In '{0}', Unexisting index '{1}', because it is Empty !";
    public override string Message { get; }

    public UnexistingIndexBecauseEmptyException(int index, string subject)
    {
        Message = string.Format(MESSAGE_FORMAT, subject, index);
    }
}