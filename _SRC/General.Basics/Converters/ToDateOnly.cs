using General.Basics.ErrorHandling;


namespace General.Basics.Converters;

public class ToDateOnly
{
    private const string DATE_DEFAULT_LABEL = "Date";

    public static Result<DateOnly> FromString(string? str, string expectedFormat, string subjectLabel = DATE_DEFAULT_LABEL)
    {
        if (string.IsNullOrWhiteSpace(str)
            || !DateOnly.TryParseExact(str, expectedFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateOnly asDateOnly))
            return Result<DateOnly>.NotOk(Errors.InvalidDate(str, $"{subjectLabel} (expected format: '{expectedFormat}')"));

        return Result<DateOnly>.Ok(asDateOnly);
    }

    static class Errors
    {
        public static InvalidDateError InvalidDate(string? dataInvalidValue, string subjectLabel) => new(dataInvalidValue, subjectLabel);
    }

}
