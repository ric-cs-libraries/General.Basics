using Xunit;


namespace General.Basics.ErrorHandling.UnitTests;

public class ResultTests
{
    private static readonly Error ERROR = new Error(code: "20", debugMessageTemplate: "A");

    [Fact]
    public void ResultOk__ShouldInitializeFieldsCorrectly()
    {
        Result result = Result.Ok();

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
        Assert.Null(result.Errors);
    }

    [Fact]
    public void ResultNotOk__ShouldInitializeFieldsCorrectly()
    {
        Result result = Result.NotOk(ERROR);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(ERROR, result.Error);
        Assert.Equal(ERROR, result.Errors![0]);
    }

    [Fact]
    public void ResultImplicitOperatorForError__ShouldConvertIntoAResultWithError()
    {
        Error error = new("errorCode", "debugMessageTemplate: {0}", new[] { "text0" });
        Result result = error; //Implicit operator :  Error -> Result

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
        Assert.Equal(error, result.Errors![0]);
    }

    [Fact]
    public void ResultOfTOk_WhenValueIsNotNull_ShouldInitializeFieldsCorrectly()
    {
        var value = 150;
        Result<int> result = Result<int>.Ok(value);

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
        Assert.Null(result.Errors);
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void ResultOfTOk_WhenValueIsNotNull_ShouldInitializeFieldsCorrectly_2()
    {
        MyClass value = new();
        Result<MyClass> result = Result<MyClass>.Ok(value);

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
        Assert.Null(result.Errors);
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void ResultOfTOk_WhenValueIsNull_ShouldInitializeFieldsCorrectly()
    {
        MyClass? value = null;
        Result<MyClass> result = Result<MyClass>.Ok(value!);

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
        Assert.Null(result.Errors);
        Assert.Equal(value, result.Value);
        Assert.Null(result.Value);
    }

    [Fact]
    public void ResultOfTNotOk__ShouldInitializeFieldsCorrectly()
    {
        Result<MyClass> result = Result<MyClass>.NotOk(ERROR);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(ERROR, result.Error);
        Assert.Equal(ERROR, result.Errors![0]);
    }

    [Fact]
    public void ResultOfT_WhenNotOkAndTryingToAccessTheValueProperty_ShouldThrowAnUnavailableResultValueException()
    {
        Result<MyClass> result = Result<MyClass>.NotOk(ERROR);

        var ex = Assert.Throws<UnavailableResultValueException>(() => result.Value);
        Assert.Equal(string.Format(UnavailableResultValueException.MESSAGE, ERROR.ToString()), ex.Message);
    }

    [Fact]
    public void ResultOfTImplicitOperatorForTValue__ShouldConvertValueIntoAResultOfTValue()
    {
        int value = 150;
        Result<int> result = value;

        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void ResultOfTImplicitOperatorForTValue__ShouldConvertValueIntoAResultOfTValue_2()
    {
        MyClass value = new();
        Result<MyClass> result = value;

        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void ResultOfTImplicitOperatorForError__ShouldConvertIntoAResultOfTValueWithError()
    {
        Error error = new("errorCode", "debugMessageTemplate: {0}", new[] { "text0" });
        Result<MyClass> result = error; //Implicit operator :  Error -> Result<T>

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
        Assert.Equal(error, result.Errors![0]);

        var ex = Assert.Throws<UnavailableResultValueException>(() => result.Value);
        Assert.Equal(string.Format(UnavailableResultValueException.MESSAGE, error.ToString()), ex.Message);
    }


    [Fact]
    public void ResultOfTImplicitOperatorForResultOfT_WhenResultOk__ShouldReturnTheResultOfTValue()
    {
        //--- Arrange ---
        MyClass value = new();
        Result<MyClass> result = Result<MyClass>.Ok(value);

        //--- Act ---
        MyClass? obj = result;

        //--- Assert ---
        Assert.Equal(result.Value, obj);
    }

    [Fact]
    public void ResultOfTImplicitOperatorForResultOfT_WhenNotOkAndTryingToReadTheValueProperty_ShouldThrowAnUnavailableResultValueException()
    {
        Result<MyClass> result = Result<MyClass>.NotOk(ERROR);

        var ex = Assert.Throws<UnavailableResultValueException>(() => { MyClass? v = result; });   //Implicit Operator : Result<MyClass> ---> MyClass
        Assert.Equal(string.Format(UnavailableResultValueException.MESSAGE, ERROR.ToString()), ex.Message);
    }
}

class MyClass
{

}
