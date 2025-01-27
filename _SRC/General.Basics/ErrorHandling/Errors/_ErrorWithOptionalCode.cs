namespace General.Basics.ErrorHandling;

public record ErrorWithOptionalCode : Error
{
    protected override bool IsCodeMandatory => false;

    public ErrorWithOptionalCode()
        : base(string.Empty, string.Empty, null)
    {
    }
    public ErrorWithOptionalCode(string debugMessageTemplate)
        : base(string.Empty, debugMessageTemplate, null)
    {
    }

    public ErrorWithOptionalCode(string debugMessageTemplate, IEnumerable<string?>? placeholderValues)
        : base(string.Empty, debugMessageTemplate, placeholderValues)
    {
    }


    //--- With provided Code ---

    public ErrorWithOptionalCode(string code, string debugMessageTemplate)
        : base(code, debugMessageTemplate, null)
    {
    }

    public ErrorWithOptionalCode(string code, string debugMessageTemplate, IEnumerable<string?>? placeholderValues)
        : base(code, debugMessageTemplate, placeholderValues)
    {
    }

    public ErrorWithOptionalCode(string code, Error error) : base(code, error)
    {
    }
}