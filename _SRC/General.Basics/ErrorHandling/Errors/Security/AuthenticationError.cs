namespace General.Basics.ErrorHandling;

public record AuthenticationError : ErrorWithOptionalCode
{
    private const string DEFAULT_DEBUG_MESSAGE_TEMPLATE = "Authentication Error";

    public AuthenticationError(string debugMessageTemplate = DEFAULT_DEBUG_MESSAGE_TEMPLATE, IEnumerable<string?>? placeholderValues = null, string code = "")
        : base(code, debugMessageTemplate, placeholderValues)
    {
    }

    public AuthenticationError(string code, Error error) : base(code, error)
    {
    }
}
