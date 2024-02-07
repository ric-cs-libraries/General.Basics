namespace Transverse._Common.General.Basics.Exceptions;

public class InvalidNegativeIndexException : Exception
{
    public override string Message { get; }

    public InvalidNegativeIndexException(int negativeIndex) : base("")
    {
        Message = $"Negative index({negativeIndex}) unauthorized.";
    }
}