namespace General.Basics.ErrorHandling;

public record InvalidGuidError : InvalidDataError
{
    public InvalidGuidError(string debugMessageTemplate, IEnumerable<string?>? placeholderValues = null)
        : base(debugMessageTemplate, placeholderValues)
    {
    }

    //

    public InvalidGuidError(string? invalidGuid, string guidLabel, string code = "", string debugMessageTemplate = DEFAULT_DEBUG_TEMPLATE_MESSAGE)
        : base(invalidGuid, guidLabel, code, debugMessageTemplate)
    {
    }

    public InvalidGuidError(string code, Error error) : base(code, error)
    {
    }
}
