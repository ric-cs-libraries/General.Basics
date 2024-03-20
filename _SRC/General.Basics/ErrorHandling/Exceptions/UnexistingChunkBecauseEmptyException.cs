namespace General.Basics.ErrorHandling;

public class UnexistingChunkBecauseEmptyException : Exception
{
    public const string MESSAGE_FORMAT = "In {0}, Unexisting Chunk [startIndex='{1}'; endIndex='{2}'] ; because it is Empty !";
    public override string Message { get; }

    public UnexistingChunkBecauseEmptyException(int startIndex, int endIndex, string subject)
    {
        Message = string.Format(MESSAGE_FORMAT, subject, startIndex, endIndex);
    }
}