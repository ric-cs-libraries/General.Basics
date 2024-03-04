namespace General.Basics.Checks;

public static class Check
{
    public const string NOT_NULL_MESSAGE_FORMAT = "The variable '{0}' is null but must not !";
    public static void NotNull(object x, string variableName)
    {
        _ = x ?? throw new NullReferenceException( string.Format(NOT_NULL_MESSAGE_FORMAT, variableName) );
    }
}
