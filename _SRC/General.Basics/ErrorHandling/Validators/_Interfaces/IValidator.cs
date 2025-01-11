namespace General.Basics.ErrorHandling.Validators.Interfaces;

public interface IValidator<T>
{
    bool IsNullAccepted { get; }

    bool IsValid(T value);

    Result Validate(T value, string subjectLabel);
}
