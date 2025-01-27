namespace General.Basics.ErrorHandling;

public record InvalidDateError : InvalidDataError
{
    public InvalidDateError(string debugMessageTemplate, IEnumerable<string?>? placeholderValues = null)
        : base(debugMessageTemplate, placeholderValues)
    {
    }

    //

    public InvalidDateError(string? invalidDate, string dateLabel, string code = "", string debugMessageTemplate = DEFAULT_DEBUG_TEMPLATE_MESSAGE)
        : base(invalidDate, dateLabel, code, debugMessageTemplate)
    {
    }

    public InvalidDateError(string code, Error error) : base(code, error)
    {
    }
}
