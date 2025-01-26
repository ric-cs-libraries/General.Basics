namespace General.Basics.ErrorHandling;

public class UnexistingChunkException : Exception
{
    public const string MESSAGE_FORMAT = "In {0}, Unexisting Chunk [startIndex='{1}'; endIndex='{2}'] ; possible range : [{3},{4}].";
    public override string Message { get; }

    public UnexistingChunkException(int startIndex, int? endIndex, int minIndex, int maxIndex, string subject)
    {
        Message = string.Format(MESSAGE_FORMAT, subject, startIndex, endIndex, minIndex, maxIndex);
    }
}