using General.Basics.ErrorHandling;

namespace General.Basics.Converters;

public class ToGuid
{
    private const string DEFAULT_SUBJECT_LABEL = "GUID";

    public static Result<Guid> FromString(string? str, string subjectLabel = DEFAULT_SUBJECT_LABEL)
    {
        Guid asGuid = Guid.Empty;
        bool isValid = !string.IsNullOrWhiteSpace(str) && Guid.TryParse(str, out asGuid);

        if (!isValid) 
        {
            InvalidGuidError error = new(str, subjectLabel);
            return Result<Guid>.NotOk(error);
        }

        var result = Result<Guid>.Ok(asGuid);
        return result;
    }

}
