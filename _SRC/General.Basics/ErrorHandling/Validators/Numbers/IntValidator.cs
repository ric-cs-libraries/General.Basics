using General.Basics.ErrorHandling.Validators.Interfaces;
using General.Basics.Bounds.Intervals;


namespace General.Basics.ErrorHandling.Validators;

public record IntValidator : IValidator<int?>
{
    public bool IsNullAccepted { get; }

    public bool HasInterval => (IntsInterval is not null);

    public IntsInterval? IntsInterval { get; }

    public IntValidator(bool isNullAccepted, IntsInterval? intsInterval = null)
    {
        IsNullAccepted = isNullAccepted;
        IntsInterval = intsInterval;
    }

    public bool IsValid(int? @int)
    {
        Result result = Validate(@int, string.Empty);
        return result.IsSuccess;
    }

    public Result Validate(int? @int, string subjectLabel)
    {
        if (!IsNullAccepted && (@int is null))
            return Result.NotOk(Errors.NullNotAccepted(@int, subjectLabel));


        if (HasInterval)
        {
            if (!IntsInterval!.Contains(@int))
            {
                var errLabel = $"""
                    {subjectLabel} {@int} was expected in interval : 
                    [{(IntsInterval!.MinValue)},{(IntsInterval!.MaxValue)}].
                    """;
                return Result.NotOk(Errors.OutOfIntervalInt(errLabel));
            }
        }

        return Result.Ok();
    }

    static class Errors
    {
        public static InvalidDataError NullNotAccepted(int? dataInvalidValue, string subjectLabel) =>
            new("int.validator.nullNotAccepted", $"{dataInvalidValue}", subjectLabel);

        public static InvalidDataError OutOfIntervalInt(string errLabel) =>
            new("int.validator.outOfInterval", string.Empty, string.Empty, debugMessageTemplate: errLabel);
    }
}
