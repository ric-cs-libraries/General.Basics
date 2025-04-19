using General.Basics.Extensions;

namespace General.Basics.ErrorHandling;


public class Result
{
    public bool IsSuccess => (Errors is null) || !Errors.Any();
    public bool IsFailure => !IsSuccess;

    public Error? Error => IsFailure ? Errors!.First() : null;   //First Error or null.

    private List<Error>? _errors;
    public IReadOnlyList<Error>? Errors => _errors?.AsReadOnly();
    public override string ToString()
    {
        string errors = (IsFailure) ? $"'{string.Join("', '", Errors!.Select(error => (error.Code.IsEmpty_()) ? error.Type : error.Code) ?? Enumerable.Empty<string>())}'" : string.Empty;
        var response = $"IsSuccess={IsSuccess}; Errors=[{errors}]";
        return response;
    }

    public string ErrorsToString()
    {
        string errors = (IsFailure) ? Errors!.Select(error => error.DebugMessage).ToStringAsArray_() : string.Empty;
        return errors;
    }

    //----------------------------------------------

    //Rien à retourner et pas d'erreur.
    public static Result Ok() => new();

    //Rien à retourner mais, une erreur est survenue.
    public static Result NotOk(Error error) => new(error);

    //Rien à retourner mais, au moins une erreur est survenue.
    public static Result NotOk(IEnumerable<Error> errors) => new(errors);


    public static Result WithAllErrorsOrOk(params Result[] results)
    {
        List<Error> allErrors = new();
        foreach (Result result in results)
        {
            if (result.IsFailure)
                allErrors.AddRange(result.Errors!);
            ;
        }

        if (allErrors.Any())
            return NotOk(allErrors)
        ;
        return Ok();
    }

    public static Result FirstWithErrorOrOk(params Func<Result>[] fResults)
    {
        Result result;
        foreach (Func<Result> fResult in fResults)
        {
            result = fResult();
            if (result.IsFailure)
                return result;
            ;
        }

        return Ok();
    }

    public static Result ErrorOrOk<TError>(Func<bool> fErrorCondition, Func<TError> fOnError) where TError : Error
    {
        if (fErrorCondition())
            return NotOk(fOnError())
        ;
        return Ok();
    }

    public static Result ErrorsOrOk(Func<bool> fErrorCondition, Func<IEnumerable<Error>> fOnError)
    {
        if (fErrorCondition())
            return NotOk(fOnError())
        ;
        return Ok();
    }

    //----------------------------------------------

    protected Result()
    {
    }
    protected Result(Error error) : this(new[] { error })
    {
    }
    protected Result(IEnumerable<Error> errors)
    {
        _errors = errors.ToList();
    }

    //----------------------------------------------

    public static implicit operator Result(Error error) => Result.NotOk(error);
    //public static implicit operator Result(IEnumerable<Error> errors) => Result.NotOk(errors); //INTERDIT car type interface (IEnumerable).
}


public class Result<TValue> : Result
{
    private readonly TValue _value = default!;

    public TValue Value
    {
        /// <exception cref="UnavailableResultValueException"></exception>
        get
        {
            if (!IsSuccess)
                throw new UnavailableResultValueException(this);
            return _value;
        }
    }
    public override string ToString()
    {
        string value = (IsSuccess) ? _value!.ToString()! : string.Empty;
        var response = $"{base.ToString()} ; Value='{value}'";
        return response;
    }


    //----------------------------------------------

    //Une valeur à retourner (donc pas d'erreur).
    public static Result<TValue> Ok(TValue value) => new(value);

    //Pas de valeur renseignée, donc une erreur est survenue.
    public static new Result<TValue> NotOk(Error error) => new(error);

    //Pas de valeur renseignée, donc au moins une erreur est survenue.
    public static new Result<TValue> NotOk(IEnumerable<Error> errors) => new(errors);

    //
    public static new Result<TValue> WithAllErrorsOrOk(params Result[] results)
    {
        Result resultWithAllErrorsOrOk = Result.WithAllErrorsOrOk(results);

        if (resultWithAllErrorsOrOk.IsFailure)
            return NotOk(resultWithAllErrorsOrOk.Errors!)
        ;
        return Ok(default!);
    }

    public static new Result<TValue> FirstWithErrorOrOk(params Func<Result>[] fResults)
    {
        Result firstResultWithErrorOrOk = Result.FirstWithErrorOrOk(fResults);

        if (firstResultWithErrorOrOk.IsFailure)
            return NotOk(firstResultWithErrorOrOk.Errors!)
        ;
        return Ok(default!);
    }

    public static new Result<TValue> ErrorOrOk<TError>(Func<bool> fErrorCondition, Func<TError> fOnError) where TError : Error
    {
        if (fErrorCondition())
            return NotOk(fOnError())
        ;
        return Ok(default!);
    }

    public static new Result<TValue> ErrorsOrOk(Func<bool> fErrorCondition, Func<IEnumerable<Error>> fOnError)
    {
        if (fErrorCondition())
            return NotOk(fOnError())
        ;
        return Ok(default!);
    }

    //----------------------------------------------

    protected internal Result(TValue value) : base()
    {
        _value = value;
    }

    protected internal Result(Error error) : base(error)
    {
    }
    protected internal Result(IEnumerable<Error> errors) : base(errors)
    {
    }

    //----------------------------------------------

    public static implicit operator Result<TValue>(TValue value) => new(value);
    public static implicit operator Result<TValue>(Error error) => Result<TValue>.NotOk(error);
    //public static implicit operator Result<TValue>(IEnumerable<Error> errors) => Result<TValue>.NotOk(errors); //INTERDIT car type interface (IEnumerable).

    public static implicit operator TValue(Result<TValue> result) => result.Value;


    public static Result<TValue> FromResult(Result<TValue> result) => result;
    public static Result<TValue> FromResult(Result result) //If result is of real type Result<TValue> then : this method won't be used, but the overload method above will.
    {
        if (result.IsFailure)
            return new(result.Errors!) //The only interesting part in that case.
        ;
        return new(default(TValue)!); //Valeur bidon en l'occurence, car le Result provient d'un type Result.
    }


}