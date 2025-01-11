namespace General.Basics.ErrorHandling.Validators.Interfaces;

public interface IDateValidator : IValidator<string?>
{
    bool IsValid(string? str, string expectedFormat);

    Result Validate(string? str, string expectedFormat, string subjectLabel);
}
