namespace General.Basics.ErrorHandling;

public record InvalidDateError : InvalidDataError
{
    private const string CODE = "invalid.date";

    public InvalidDateError(string? invalidDate, string dateLabel = "Date") : base(CODE, invalidDate, dateLabel)
    {
    }
}
