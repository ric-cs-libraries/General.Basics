namespace General.Basics.ErrorHandling.Validators;

public record EmptyableDateValidator : DateValidator
{
    public EmptyableDateValidator() : base(isEmptyable: true)
    {

    }
}
