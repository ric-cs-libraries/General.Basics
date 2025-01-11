
namespace General.Basics.ErrorHandling.Validators;

public class StringParamsValidation
{
    public static Result Validate(
        IEnumerable<KeyValuePair<string, string?>> stringParams,
        IEnumerable<StringParamValidation> stringParamValidations
    )
    {
        List<Error> errors = new();
        foreach (var stringParam in stringParams)
        {
            Result validationResult = Validate(stringParam, stringParamValidations);
            if (validationResult.IsFailure)
            {
                errors.AddRange(validationResult.Errors!);
            }
        }

        if (errors.Any())
            return Result.NotOk(errors)
        ;

        return Result.Ok();
    }

    private static Result Validate(
        KeyValuePair<string, string?> stringParam,
        IEnumerable<StringParamValidation> stringParamValidations
    )
    {
        List<Error> errors = new();
        foreach (var stringParamValidation in stringParamValidations)
        {
            Result validationResult = stringParamValidation.Validate(stringParam);
            if (validationResult.IsFailure)
            {
                errors.AddRange(validationResult.Errors!);
            }
        }

        if (errors.Any())
            return Result.NotOk(errors)
        ;

        return Result.Ok();
    }
}
