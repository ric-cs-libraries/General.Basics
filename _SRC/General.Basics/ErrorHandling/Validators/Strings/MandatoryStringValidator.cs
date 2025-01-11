


namespace General.Basics.ErrorHandling.Validators;

public record MandatoryStringValidator : StringValidator
{
    public MandatoryStringValidator() : base(isEmptyable: false)
    {
    }
}
