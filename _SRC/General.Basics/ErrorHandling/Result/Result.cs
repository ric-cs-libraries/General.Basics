

namespace General.Basics.ErrorHandling;


public class Result
{
    public bool IsSuccess => (Error is null);
    public bool IsFailure => !IsSuccess;

    public Error? Error { get; }


    //Rien à retourner mais, une erreur est survenue.
    public static Result NotOk(Error error) => new(error);
    //Rien à retourner et pas d'erreur.
    public static Result Ok() => new();


    protected Result(Error error)
    {
        Error = error;
    }
    protected Result()
    {
    }

    public static implicit operator Result(Error error) => Result.NotOk(error);
}

public class Result<TValue> : Result
{
    private readonly TValue _value;

    public TValue Value
    {
        get
        {
            if (!IsSuccess)
                throw new UnavailableResultValueException();

            return _value;
        }
    }

    //Qqch à retourner mais, une erreur est survenue.
    public static new Result<TValue> NotOk(Error error) => new(error);
    //Qqch à retourner et pas d'erreur.
    public static Result<TValue> Ok(TValue value) => new(value);

    public static implicit operator Result<TValue>(TValue value) => new(value);
    public static implicit operator Result<TValue>(Error error) => Result<TValue>.NotOk(error);

    public static implicit operator TValue(Result<TValue> result) => result.Value;

    protected internal Result(TValue value) : base()
    {
        _value = value;
    }

    protected internal Result(Error error) : base(error)
    {
    }
}