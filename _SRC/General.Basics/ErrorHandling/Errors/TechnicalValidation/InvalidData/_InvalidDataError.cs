
namespace General.Basics.ErrorHandling;

public record InvalidDataError : ErrorWithOptionalCode
{
    protected const string DEFAULT_DEBUG_TEMPLATE_MESSAGE = "'{0}' is not a valid {1} value.";

    public InvalidDataError(string debugMessageTemplate, IEnumerable<string?>? placeholderValues = null)
        : base(debugMessageTemplate, placeholderValues)
    {
    }

    //

    public InvalidDataError(string? dataInvalidValue, string dataLabel, string code = "", string debugMessageTemplate = DEFAULT_DEBUG_TEMPLATE_MESSAGE)
        : base(code, debugMessageTemplate, new[] { dataInvalidValue, dataLabel })
    {
    }
}
