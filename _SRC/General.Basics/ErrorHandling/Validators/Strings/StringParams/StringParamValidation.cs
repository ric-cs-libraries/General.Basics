using System.Text.RegularExpressions;



namespace General.Basics.ErrorHandling.Validators;

public record StringParamValidation
{
    public Predicate<string> StringParamNamePredicate { get; }

    public StringValidator StringParamValueValidator { get; }


    public StringParamValidation(Predicate<string> stringParamNamePredicate, StringValidator stringParamValueValidator)
    {
        StringParamNamePredicate = stringParamNamePredicate;
        StringParamValueValidator = stringParamValueValidator;
    }

    public StringParamValidation(string paramName, StringValidator stringParamValueValidator) : this(
        (string paramName_) => (paramName_.ToUpper() == paramName.ToUpper()),
        stringParamValueValidator
    )
    {
    }

    public StringParamValidation(Regex paramNameRegExp, StringValidator stringParamValueValidator) : this(
        (string paramName) => paramNameRegExp.IsMatch(paramName),
        stringParamValueValidator
    )
    {
    }

    public Result Validate(KeyValuePair<string, string?> stringParam)
    {
        if (StringParamNamePredicate(stringParam.Key))
        {
            Result validationResult = StringParamValueValidator.Validate(stringParam.Value, stringParam.Key);
            if (validationResult.IsFailure)
                return Result.NotOk(validationResult.Errors!);
        }
        return Result.Ok();
    }

}
