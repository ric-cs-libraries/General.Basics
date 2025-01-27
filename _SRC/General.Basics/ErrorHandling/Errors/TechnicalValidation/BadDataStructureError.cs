namespace General.Basics.ErrorHandling;

public record BadDataStructureError : ErrorWithOptionalCode
{
    public BadDataStructureError(string debugMessageTemplate, IEnumerable<string?>? placeholderValues = null)
        : base(debugMessageTemplate, placeholderValues)
    {
    }

    //

    public BadDataStructureError(string code, string debugMessageTemplate, IEnumerable<string>? placeholderValues = null)
        : base(code, debugMessageTemplate, placeholderValues)
    {
    }

    public BadDataStructureError(Error error, string code = "") : base(error, code)
    {
    }
}
