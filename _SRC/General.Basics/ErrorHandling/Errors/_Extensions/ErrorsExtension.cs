using General.Basics.Extensions;

namespace General.Basics.ErrorHandling.Errors.Extensions;

public static class ErrorsExtension
{
    public const string DEFAULT_SEPARATOR_BETWEEN_ERRORS = " / ";

    public static Exception ToException(this IEnumerable<Error> errors, string separatorBetweenErrors = DEFAULT_SEPARATOR_BETWEEN_ERRORS)
    {
        IEnumerable<string> errorsInfos = errors.Select(error => error.ToString());
        string exceptionMsg = string.Join(separatorBetweenErrors, errorsInfos);
        Exception ex = new Exception(exceptionMsg);
        SetExceptionDictionaryInfos(errors, ex);
        return ex;
    }

    public static Exception ToException(this Error error)
    {
        Exception ex = new Exception(error.ToString());
        SetExceptionDictionaryInfos(new[] { error }, ex);
        if (int.TryParse(error.Code, out int errorCodeAsInt))
        {
            ex.HResult = errorCodeAsInt;
        }
        return ex;
    }

    private static void SetExceptionDictionaryInfos(IEnumerable<Error> errors, Exception ex)
    {
        errors.ToList().ForEach(error =>
        {
            if (!error.Code.IsEmpty_() && !ex.Data.Contains(error.Code))
            {
                ex.Data[error.Code] = error.ToString();
            }
        });
    }
}
