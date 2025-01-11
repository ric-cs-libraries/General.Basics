namespace General.Basics.ErrorHandling;

public record ResourceNotFoundError : Error
{
    public ResourceNotFoundError(string code, string? resourceValue, string resourceLabel = "Resource", string debugMessageTemplate = "{0} '{1}' not found.")
        : this(code, debugMessageTemplate, new[] { resourceLabel, resourceValue })
    {
    }

    public ResourceNotFoundError(string code, string debugMessageTemplate = "Resource Not Found", IEnumerable<string?>? placeholderValues = null) 
        : base(code, debugMessageTemplate, placeholderValues)
    {
    }
}
