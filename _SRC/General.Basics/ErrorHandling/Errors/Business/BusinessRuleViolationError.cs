namespace General.Basics.ErrorHandling;

public record BusinessRuleViolationError : ErrorWithOptionalCode
{
    public BusinessRuleViolationError(string debugMessageTemplate = "", IEnumerable<string?>? placeholderValues = null)
        : base(debugMessageTemplate, placeholderValues)
    {
    }
}
