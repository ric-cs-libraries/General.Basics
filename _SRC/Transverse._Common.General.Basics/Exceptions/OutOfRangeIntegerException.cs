namespace Transverse._Common.General.Basics.Exceptions;

public class OutOfRangeIntegerException : Exception
{
    public override string Message { get; }

    public OutOfRangeIntegerException(int invalidIndex, int minIndexValue, int maxIndexValue, string? subject = null) : base("")
    {
        subject ??= "Number";
        Message = $"Invalid {subject} : '{invalidIndex}', possible range : [{minIndexValue},{maxIndexValue}].";
    }
}