namespace General.Basics.ErrorHandling;

public record InvalidGuidError : InvalidDataError
{
    private const string CODE = "invalid.guid";

    public InvalidGuidError(string? invalidGuid, string dataLabel = "GUID") : base(CODE, invalidGuid, dataLabel)
    {
    }
}
