namespace General.Basics.ErrorHandling;

public record BusinessRuleViolationError : Error
{
    public BusinessRuleViolationError(string code, string debugMessageTemplate = "Business Rule Violation", IEnumerable<string?>? placeholderValues = null) 
        : base(code, debugMessageTemplate, placeholderValues)
    {
    }

    public BusinessRuleViolationError(string code, string dataLabel, string? dataInvalidValue, string debugMessageTemplate = "'{0}' is not a valid {1} value.")
        : this(code, debugMessageTemplate, new[] { dataInvalidValue, dataLabel })
    {
    }
}
