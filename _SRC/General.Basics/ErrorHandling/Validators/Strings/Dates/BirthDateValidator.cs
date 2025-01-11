using General.Basics.Intervals;

namespace General.Basics.ErrorHandling.Validators;

public record BirthDateValidator : DateValidator
{
    public BirthDateValidator(bool isEmptyable) 
        : base(isEmptyable, new DateTimesInterval(minValue: null, maxValue: DateTime.UtcNow.AddDays(-1)))
    {
    }

}
