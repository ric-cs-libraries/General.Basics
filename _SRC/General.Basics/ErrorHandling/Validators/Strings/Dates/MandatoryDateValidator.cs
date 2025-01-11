namespace General.Basics.ErrorHandling.Validators;

public record MandatoryDateValidator : DateValidator
{
    public MandatoryDateValidator() : base(isEmptyable: false)
    {

    }
}
