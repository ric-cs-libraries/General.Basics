using General.Basics.ErrorHandling.Validators.Interfaces;



namespace General.Basics.ErrorHandling.Validators;

public record StringValidator : IValidator<string?>
{
    public bool IsNullAccepted { get; }
    public bool IsEmptyAccepted { get; }
    public bool IsOnlySpacesAccepted { get; }

    public bool isEmptyable => (IsNullAccepted && IsEmptyAccepted && IsOnlySpacesAccepted);

    public StringValidator(bool isNullAccepted, bool isEmptyAccepted, bool isOnlySpacesAccepted)
    {
        IsNullAccepted = isNullAccepted;
        IsEmptyAccepted = isEmptyAccepted;
        IsOnlySpacesAccepted = isOnlySpacesAccepted;
    }
    public StringValidator(bool isEmptyable) : this(isNullAccepted: isEmptyable, isEmptyAccepted: isEmptyable, isOnlySpacesAccepted: isEmptyable)
    {
    }

    public virtual bool IsValid(string? str)
    {
        Result result = Validate(str, string.Empty);

        return result.IsSuccess;
    }

    public virtual Result Validate(string? str, string subjectLabel)
    {
        if (!IsNullAccepted && (str is null))
            return Result.NotOk(Errors.NullNotAccepted(str, subjectLabel));

        if (!IsEmptyAccepted && (str == string.Empty))
            return Result.NotOk(Errors.EmptyNotAccepted(str, subjectLabel));

        if (!IsOnlySpacesAccepted && (str!.Trim() == string.Empty))
            return Result.NotOk(Errors.OnlySpacesNotAccepted(str, subjectLabel));

        return Result.Ok();
    }

    static class Errors
    {
        public static InvalidDataError NullNotAccepted(string? dataInvalidValue, string subjectLabel) =>
            new("string.validator.nullNotAccepted", dataInvalidValue, subjectLabel);
        public static InvalidDataError EmptyNotAccepted(string? dataInvalidValue, string subjectLabel) =>
            new("string.validator.emptyNotAccepted", dataInvalidValue, subjectLabel);
        public static InvalidDataError OnlySpacesNotAccepted(string? dataInvalidValue, string subjectLabel) =>
            new("string.validator.onlySpacesNotAccepted", dataInvalidValue, subjectLabel);
    }

}
