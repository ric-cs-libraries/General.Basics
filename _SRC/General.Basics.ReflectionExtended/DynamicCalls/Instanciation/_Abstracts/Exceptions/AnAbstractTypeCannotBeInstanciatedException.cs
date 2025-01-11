namespace General.Basics.ReflectionExtended.DynamicCalls.Abstracts;


public class AnAbstractTypeCannotBeInstanciatedException : Exception
{
    public const string MESSAGE_FORMAT = "An abstract type ('{0}') cannot be instanciated.";

    public override string Message { get; }

    public AnAbstractTypeCannotBeInstanciatedException(string typeFullName)
    {
        Message = string.Format(MESSAGE_FORMAT, typeFullName);
    }
}
