namespace General.Basics.ErrorHandling;

public record ResourceNotFoundError : ErrorWithOptionalCode
{
    private const string DEFAULT_DEBUG_MESSAGE_TEMPLATE = "{0} '{1}' not found.";
    private const string DEFAULT_RESOURCE_LABEL = "Resource";

    public ResourceNotFoundError
        (string debugMessageTemplate, IEnumerable<string?>? placeholderValues, string code = "")
        : base(code, debugMessageTemplate, placeholderValues)
    {
    }

    public ResourceNotFoundError
        (string? resourceValue, string resourceLabel = DEFAULT_RESOURCE_LABEL, string code = "", string debugMessageTemplate = DEFAULT_DEBUG_MESSAGE_TEMPLATE)
        : this(debugMessageTemplate, new[] { resourceLabel, resourceValue }, code)
    {
    }

    public ResourceNotFoundError(Error error, string code = "") : base(error, code)
    {
    }
}
