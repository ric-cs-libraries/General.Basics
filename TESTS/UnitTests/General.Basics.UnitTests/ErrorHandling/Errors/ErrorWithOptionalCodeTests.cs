using Xunit;



namespace General.Basics.ErrorHandling.UnitTests;

public class ErrorWithOptionalCodeTests
{
    [Theory]
    [InlineData("some.error")]
    [InlineData("some.error", "msg for dev")]
    [InlineData("some.error", "msg for dev: {0}, {1}", new[] { "text0", "text2" })]
    public void Instanciation__ShouldCorrectlyInitializeTheInstance(string code, string debugMessageTemplate = "", IEnumerable<string>? placeholderValues = null)
    {
        //--- Arrange ---
        placeholderValues = placeholderValues ?? Enumerable.Empty<string>();
        var expectedDebugMessage = string.Format(debugMessageTemplate, placeholderValues!.ToArray());

        //--- Act ---
        var error = new ErrorWithOptionalCode(code, debugMessageTemplate, placeholderValues);

        //--- Assert ---
        Assert.Equal(code, error.Code);
        Assert.Equal(debugMessageTemplate, error.DebugMessageTemplate);
        Assert.Equal(expectedDebugMessage, error.DebugMessage);
        Assert.Equal(placeholderValues, error.PlaceholderValues);
        Assert.Equal(nameof(ErrorWithOptionalCode), error.Type);
        Assert.Equal($"{nameof(ErrorWithOptionalCode)} '{code}'", error.Kind);
    }

    [Fact]
    public void Instanciation_WhenPlaceholderValuesIsNotGiven_ShouldCorrectlyInitializeTheInstanceWithANullPlaceholderValues()
    {
        //--- Arrange ---
        var code = "some.code";
        var debugMessageTemplate = "msg";

        //--- Act ---
        var error = new ErrorWithOptionalCode(code, debugMessageTemplate);

        //--- Assert ---
        Assert.Equal(code, error.Code);
        Assert.Equal(debugMessageTemplate, error.DebugMessageTemplate);
        Assert.Equal(debugMessageTemplate, error.DebugMessage);
        Assert.Empty(error.PlaceholderValues!);
        Assert.Equal(nameof(ErrorWithOptionalCode), error.Type);
        Assert.Equal($"{nameof(ErrorWithOptionalCode)} '{code}'", error.Kind);
    }

    [Fact]
    public void Instanciation_WhenOnlyDebugMessageTemplateIsGiven_ShouldShouldCorrectlyInitializeTheInstance()
    {
        //--- Arrange ---
        var debugMessageTemplate = "someMsg";

        //--- Act ---
        var error = new ErrorWithOptionalCode(debugMessageTemplate);

        //--- Assert ---
        Assert.Empty(error.Code);
        Assert.Equal(debugMessageTemplate, error.DebugMessageTemplate);
        Assert.Equal(debugMessageTemplate, error.DebugMessage);
        Assert.Empty(error.PlaceholderValues!);
        Assert.Equal(nameof(ErrorWithOptionalCode), error.Type);
        Assert.Equal($"{nameof(ErrorWithOptionalCode)}", error.Kind);
    }

    [Fact]
    public void Instanciation_WhenOnlyCodeIsNotGiven_ShouldShouldCorrectlyInitializeTheInstance()
    {
        //--- Arrange ---
        var debugMessageTemplate = "someMsg : {0} va {1}";
        var placeholderValues = new[] { "ça ne", "pas" };

        //--- Act ---
        var error = new ErrorWithOptionalCode(debugMessageTemplate, placeholderValues);

        //--- Assert ---
        var expectedDebugMessage = string.Format(debugMessageTemplate, placeholderValues!.ToArray());
        Assert.Empty(error.Code);
        Assert.Equal(debugMessageTemplate, error.DebugMessageTemplate);
        Assert.Equal(expectedDebugMessage, error.DebugMessage);
        Assert.Equal(placeholderValues, error.PlaceholderValues!);
        Assert.Equal(nameof(ErrorWithOptionalCode), error.Type);
        Assert.Equal($"{nameof(ErrorWithOptionalCode)}", error.Kind);
    }

    [Fact]
    public void Instanciation_WhenNoParamIsGivenOrOnlyCodeIsGivenButEmpty_ShouldShouldCorrectlyInitializeTheInstance()
    {
        //--- Arrange ---
        var debugMessageTemplate = string.Empty;

        //--- Act ---
        var error1 = new ErrorWithOptionalCode(string.Empty);
        var error2 = new ErrorWithOptionalCode();

        //--- Assert ---
        Assert.Empty(error1.Code);
        Assert.Empty(error1.DebugMessageTemplate);
        Assert.Empty(error1.DebugMessage);
        Assert.Empty(error1.PlaceholderValues!);
        Assert.Equal(nameof(ErrorWithOptionalCode), error1.Type);
        Assert.Equal($"{nameof(ErrorWithOptionalCode)}", error1.Kind);

        Assert.Empty(error2.Code);
        Assert.Empty(error2.DebugMessageTemplate);
        Assert.Empty(error2.DebugMessage);
        Assert.Empty(error2.PlaceholderValues!);
        Assert.Equal(nameof(ErrorWithOptionalCode), error2.Type);
        Assert.Equal($"{nameof(ErrorWithOptionalCode)}", error2.Kind);
    }

    [Fact]
    public void Instanciation_WhenAnErrorAndACodeAreGiven_ShouldCorrectlyInitializeTheInstance()
    {
        //--- Arrange---
        string debugMessageTemplate = "Customer name length must be between {0} and {1}.";
        IEnumerable<string> placeholderValues = new[] { "2", "70" };
        BusinessRuleViolationError businessRuleViolationError = new(debugMessageTemplate, placeholderValues);
        var applicationUseCaseErrorCode = "invalid.customer.name.length";

        //--- Act ---
        var error = new ErrorWithOptionalCode(applicationUseCaseErrorCode, businessRuleViolationError);

        //--- Assert ---
        var expectedDebugMessage = string.Format(debugMessageTemplate, placeholderValues.ToArray()) + $" (from {businessRuleViolationError.Type})";
        Assert.Equal(applicationUseCaseErrorCode, error.Code);
        Assert.Equal(expectedDebugMessage, error.DebugMessageTemplate);
        Assert.Equal(expectedDebugMessage, error.DebugMessage);
        Assert.Empty(error.PlaceholderValues!);
        Assert.Equal(nameof(ErrorWithOptionalCode), error.Type);
        Assert.Equal($"{nameof(ErrorWithOptionalCode)} '{applicationUseCaseErrorCode}'", error.Kind);
    }

    [Fact]
    public void Instanciation_WhenAnErrorAndACodeAreGiven_ShouldCorrectlyInitializeTheInstance_2()
    {
        //--- Arrange---
        Fixtures.CustomerNameLengthError customerNameLengthError = new(minLength: 2, maxLength: 70);
        var applicationUseCaseErrorCode = Fixtures.ApplicationCreateCustomerUseCase_NameLengthError.ERROR_CODE;

        //--- Act ---
        var errorForWebApi = new ErrorWithOptionalCode(applicationUseCaseErrorCode, customerNameLengthError);

        //--- Assert ---
        var expectedDebugMessage = customerNameLengthError.DebugMessage + $" (from {customerNameLengthError.Type})";
        Assert.Equal(applicationUseCaseErrorCode, errorForWebApi.Code);
        Assert.Equal(expectedDebugMessage, errorForWebApi.DebugMessageTemplate);
        Assert.Equal(expectedDebugMessage, errorForWebApi.DebugMessage);
        Assert.Empty(errorForWebApi.PlaceholderValues!);
        Assert.Equal(nameof(ErrorWithOptionalCode), errorForWebApi.Type);
        Assert.Equal($"{nameof(ErrorWithOptionalCode)} '{applicationUseCaseErrorCode}'", errorForWebApi.Kind);
    }

    #region ToString
    [Fact]
    public void ToString_WhenEveryParamHasBeenGivenToConstructor_ShouldReturnTheCorrectValue()
    {
        //--- Arrange ---
        var errorCode = "some.code";
        var debugMessageTemplate = "myDebugMessageTemplate : {0} and {1}";
        var placeholderValues = new[] { "txt1", "txt2" };

        //--- Act ---
        var error = new ErrorWithOptionalCode(errorCode, debugMessageTemplate, placeholderValues);

        //--- Assert ---
        var expected = $"{nameof(ErrorWithOptionalCode)} '{errorCode}' : {error.DebugMessage}";
        Assert.Equal(expected, error.ToString());
    }

    [Fact]
    public void ToString_WhenCodeIsGivenAndDebugMessageTemplateIsEmpty_ShouldReturnTheCorrectValue()
    {
        //--- Arrange ---
        var errorCode = "some.code";
        var debugMessageTemplate = string.Empty;

        //--- Act ---
        var error = new ErrorWithOptionalCode(errorCode, debugMessageTemplate);

        //--- Assert ---
        var expected = $"{nameof(ErrorWithOptionalCode)} '{errorCode}'";
        Assert.Equal(expected, error.ToString());
    }

    [Fact]
    public void ToString_WhenOnlyCodeAndDebugMessageTemplateAreGiven_ShouldReturnTheCorrectValue()
    {
        //--- Arrange ---
        var errorCode = "some.code";
        var debugMessageTemplate = "SomeMsg";

        //--- Act ---
        var error = new ErrorWithOptionalCode(errorCode, debugMessageTemplate);

        //--- Assert ---
        var expected = $"{nameof(ErrorWithOptionalCode)} '{errorCode}' : {debugMessageTemplate}";
        Assert.Equal(expected, error.ToString());
    }

    [Fact]
    public void ToString_WhenOnlyDebugMessageTemplateIsGiven_ShouldReturnTheCorrectValue()
    {
        //--- Arrange ---
        var debugMessageTemplate = string.Empty;

        //--- Act ---
        var error = new ErrorWithOptionalCode(debugMessageTemplate);

        //--- Assert ---
        var expected = $"{nameof(ErrorWithOptionalCode)}";
        Assert.Equal(expected, error.ToString());
    }

    [Fact]
    public void ToString_NoParamWereGivenToConstructor_ShouldReturnTheCorrectValue()
    {
        //--- Act ---
        var error = new ErrorWithOptionalCode();

        //--- Assert ---
        var expected = $"ErrorWithOptionalCode";
        Assert.Equal(expected, error.ToString());
    }
    #endregion ToString

}