namespace General.Basics.Exceptions;

public class MustBePositiveIntegerException : Exception
{
    public override string Message { get; }

    public MustBePositiveIntegerException(int negativeInt, string? subject = null) : base("")
    {
        subject ??= "Number";
        Message = $"{subject} must be a >=0 integer : '{negativeInt}' unauthorized.";
    }
}