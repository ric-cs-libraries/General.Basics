namespace General.Basics.Exceptions;

public class UnexistingChunkException : Exception
{
    public override string Message { get; }

    public UnexistingChunkException(int startIndex, int endIndex, int minIndex, int maxIndex, string subject) : base("")
    {
        Message = $"In {subject}, Unexisting Chunk [startIndex='{startIndex}'; endIndex='{endIndex}'] ; possible range : [{minIndex},{maxIndex}].";
    }
}