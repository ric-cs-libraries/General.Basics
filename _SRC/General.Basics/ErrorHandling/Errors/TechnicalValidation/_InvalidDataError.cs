
namespace General.Basics.ErrorHandling;

public record InvalidDataError : Error
{
    public InvalidDataError(string code, string? dataInvalidValue, string dataLabel, string debugMessageTemplate = "'{0}' is not a valid {1} value.")
        : base(code, debugMessageTemplate, new[] { dataInvalidValue, dataLabel })
    {
    }
}
