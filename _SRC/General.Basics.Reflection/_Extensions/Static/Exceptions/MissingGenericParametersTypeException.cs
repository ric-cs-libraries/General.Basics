namespace General.Basics.Reflection.Extensions;

public class MissingGenericParametersTypeException : Exception
{
    public const string MESSAGE_FORMAT = "Méthode {0} : le type des paramètres génériques doit être mentionné. (Type générique: '{1}').";

    public override string Message { get; }

    public MissingGenericParametersTypeException(string methodName, string genericClassName)
    {
        Message = string.Format(MESSAGE_FORMAT, methodName, genericClassName);
    }
}
