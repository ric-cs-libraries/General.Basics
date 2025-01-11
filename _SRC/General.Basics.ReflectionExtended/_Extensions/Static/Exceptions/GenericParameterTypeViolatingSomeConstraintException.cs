namespace General.Basics.ReflectionExtended.Extensions;

public class GenericParameterTypeViolatingSomeConstraintException : Exception
{
    public const string MESSAGE_FORMAT = "Violation of some generic parameter type constraint : {0}";

    public override string Message { get; }

    public GenericParameterTypeViolatingSomeConstraintException(string errorMessage)
    {
        Message = string.Format(MESSAGE_FORMAT, errorMessage);
    }
}
