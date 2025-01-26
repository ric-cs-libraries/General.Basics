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
        Fixtures.MyClass value = new();
        Result<Fixtures.MyClass> result = Result<Fixtures.MyClass>.Ok(value);

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
        Assert.Null(result.Errors);
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void ResultOfTOk_WhenValueIsNull_ShouldInitializeFieldsCorrectly()
    {
        Fixtures.MyClass? value = null;
        Result<Fixtures.MyClass> result = Result<Fixtures.MyClass>.Ok(value!);

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
        Result<Fixtures.MyClass> result = Result<Fixtures.MyClass>.NotOk(ERROR);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(ERROR, result.Error);
        Assert.Equal(ERROR, result.Errors![0]);
    }

    [Fact]
    public void ResultOfT_WhenNotOkAndTryingToAccessTheValueProperty_ShouldThrowAnUnavailableResultValueException()
    {
        Result<Fixtures.MyClass> result = Result<Fixtures.MyClass>.NotOk(ERROR);

        var ex = Assert.Throws<UnavailableResultValueException>(() => result.Value);
        Assert.Equal(string.Format(UnavailableResultValueException.MESSAGE, result.ToString()), ex.Message);
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
        Fixtures.MyClass value = new();
        Result<Fixtures.MyClass> result = value;

        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void ResultOfTImplicitOperatorForError__ShouldConvertIntoAResultOfTValueWithError()
    {
        Error error = new("errorCode", "debugMessageTemplate: {0}", new[] { "text0" });
        Result<Fixtures.MyClass> result = error; //Implicit operator :  Error -> Result<T>

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
        Assert.Equal(error, result.Errors![0]);

        var ex = Assert.Throws<UnavailableResultValueException>(() => result.Value);
        Assert.Equal(string.Format(UnavailableResultValueException.MESSAGE, result.ToString()), ex.Message);
    }


    [Fact]
    public void ResultOfTImplicitOperatorForResultOfT_WhenResultOk__ShouldReturnTheResultOfTValue()
    {
        //--- Arrange ---
        Fixtures.MyClass value = new();
        Result<Fixtures.MyClass> result = Result<Fixtures.MyClass>.Ok(value);

        //--- Act ---
        Fixtures.MyClass? obj = result;

        //--- Assert ---
        Assert.Equal(result.Value, obj);
    }

    [Fact]
    public void ResultOfTImplicitOperatorForResultOfT_WhenNotOkAndTryingToReadTheValueProperty_ShouldThrowAnUnavailableResultValueException()
    {
        Result<Fixtures.MyClass> result = Result<Fixtures.MyClass>.NotOk(ERROR);

        var ex = Assert.Throws<UnavailableResultValueException>(() => { Fixtures.MyClass? v = result; });   //Implicit Operator : Result<Fixtures.MyClass> ---> Fixtures.MyClass
        Assert.Equal(string.Format(UnavailableResultValueException.MESSAGE, result.ToString()), ex.Message);
    }

    #region ToString()
    [Theory]
    [ClassData(typeof(Fixtures.ToStringClassDataWithErrors))]
    public void ToString_WhenSomeErrorsExist_ShouldReturnTheCorrectValue(IEnumerable<Error> errors, string expectedResultString)
    {
        //--- Arrange ---
        var result = Result.NotOk(errors);

        //--- Act ---
        var str = result.ToString();

        //--- Assert ---
        Assert.Equal(expectedResultString, str);
    }

    [Fact]
    public void ToString_WhenNoErrorExist_ShouldReturnTheCorrectValue()
    {
        //--- Arrange ---
        var result = Result.Ok();

        //--- Act ---
        var str = result.ToString();

        //--- Assert ---
        var expectedResultString = $"IsSuccess=True; Errors=[]";
        Assert.Equal(expectedResultString, str);
    }
    #endregion ToString()
}

static partial class Fixtures
{
    internal class ToStringClassDataWithErrors : TheoryData<IEnumerable<Error>, string>
    {
        public ToStringClassDataWithErrors()
        {
            var errorCode1 = "my.error1.code";
            var errorCode2 = "my.error2.code";

            Add(new List<Error>()
            {
                new SomeErrorWithMandatoryCode(errorCode1),
                new SomeErrorWithOptionalCode<int>(),
                new SomeErrorWithOptionalCode<int>(errorCode2),

            }, $"IsSuccess=False; Errors=['{errorCode1}', '{nameof(SomeErrorWithOptionalCode<int>)}<Int32>', '{errorCode2}']");
        }
    }

    record SomeErrorWithMandatoryCode : Error
    {
        public SomeErrorWithMandatoryCode(string code, string debugMessageTemplate = "") : base(code, debugMessageTemplate)
        {
        }
    }

    record SomeErrorWithOptionalCode<T> : ErrorWithOptionalCode
    {
        public SomeErrorWithOptionalCode()
        {
        }

        public SomeErrorWithOptionalCode(string code, string debugMessageTemplate = "") : base(code, debugMessageTemplate)
        {
        }
    }

    internal class MyClass
    {

    }
}

