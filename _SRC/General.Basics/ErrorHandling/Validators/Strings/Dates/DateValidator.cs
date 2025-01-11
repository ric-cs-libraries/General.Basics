using General.Basics.Converters;
using General.Basics.Intervals;
using General.Basics.ErrorHandling.Validators.Interfaces;


namespace General.Basics.ErrorHandling.Validators;

public record DateValidator : StringValidator, IDateValidator
{
    private const string DEFAULT_DATE_FORMAT = "dd/MM/yyyy";
    private const string DEFAULT_TIME_FORMAT = "HH:mm:ss";
    //private vconst string DEFAULT_DATETIME_FORMAT = $"{DEFAULT_DATE_FORMAT} {DEFAULT_TIME_FORMAT}";

    public static string DefaultDateFormat = DEFAULT_DATE_FORMAT;
    public static string DefaultDateTimeFormat => $"{DefaultDateFormat} {DEFAULT_TIME_FORMAT}";


    public DateTimesInterval? DateTimesInterval { get; }

    public bool HasInterval => (DateTimesInterval is not null);

    public DateValidator(bool isNullAccepted, bool isEmptyAccepted, bool isOnlySpacesAccepted, DateTimesInterval ? dateTimesInterval = null)
        : base(isNullAccepted, isEmptyAccepted, isOnlySpacesAccepted)
    {
        DateTimesInterval = dateTimesInterval;
    }

    public DateValidator(bool isEmptyable, DateTimesInterval? dateTimesInterval = null)
        : this(isNullAccepted: isEmptyable, isEmptyAccepted: isEmptyable, isOnlySpacesAccepted: isEmptyable, dateTimesInterval)
    {
    }

    public override bool IsValid(string? str)
    {
        return IsValid(str, DefaultDateFormat);
    }

    public override Result Validate(string? str, string subjectLabel)
    {
        return Validate(str, DefaultDateFormat, subjectLabel);
    }

    public bool IsValid(string? str, string expectedFormat)
    {
        Result result = Validate(str, expectedFormat, string.Empty);

        return result.IsSuccess;
    }


    public Result Validate(string? str, string expectedFormat, string subjectLabel)
    {
        Result result = base.Validate(str, subjectLabel);

        if (result.IsSuccess && !string.IsNullOrWhiteSpace(str))
        {
            Result<DateTime> dateTimeResult = ToDateTime.FromString(str, expectedFormat, subjectLabel);

            if (dateTimeResult.IsSuccess && HasInterval)
            {
                if (!DateTimesInterval!.Contains(dateTimeResult.Value))
                {
                    const string UNDEFINED_BOUND = "-";
                    DateTime? v = DateTimesInterval!.MinValue;
                    string minValue = ((DateTime?)DateTimesInterval!.MinValue!)?.ToString(expectedFormat) ?? UNDEFINED_BOUND;
                    string maxValue = ((DateTime?)DateTimesInterval!.MaxValue!)?.ToString(expectedFormat) ?? UNDEFINED_BOUND;
                    var errLabel = $"""
                    {subjectLabel} (expected format: '{expectedFormat}') {dateTimeResult.Value.ToString(expectedFormat)} was expected in interval : [ {minValue}, {maxValue} ].
                    """;
                    return Result.NotOk(Errors.OutOfIntervalDate(errLabel));
                }
            }

            return dateTimeResult;
        }

        return result;
    }

    static class Errors
    {
        public static InvalidDataError OutOfIntervalDate(string errLabel) =>
            new("date.validator.outOfInterval", string.Empty, string.Empty, debugMessageTemplate: errLabel);
    }

}
