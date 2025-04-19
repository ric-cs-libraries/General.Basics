namespace General.Basics.ErrorHandling;

public static class Check
{
    /// <exception cref="MustNotBeNullException"></exception>
    public static void NotNull(object? x, string variableName)
    {
        _ = x ?? throw new MustNotBeNullException(variableName);
    }
}
