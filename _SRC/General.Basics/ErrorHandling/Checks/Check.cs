namespace General.Basics.ErrorHandling;

public static class Check
{
    public static void NotNull(object? x, string variableName)
    {
        _ = x ?? throw new MustNotBeNullException(variableName);
    }
}
