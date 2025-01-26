using Xunit;



namespace General.Basics.ErrorHandling.UnitTests;

public class ErrorTests
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
        var error = new Error(code, debugMessageTemplate, placeholderValues);

        //--- Assert ---
        Assert.Equal(code, error.Code);
        Assert.Equal(debugMessageTemplate, error.DebugMessageTemplate);
        Assert.Equal(expectedDebugMessage, error.DebugMessage);
        Assert.Equal(placeholderValues, error.PlaceholderValues);
        Assert.Equal(nameof(Error), error.Type);
        Assert.Equal($"{nameof(Error)} '{code}'", error.Kind);
    }

    [Fact]
    public void Instanciation_WhenPlaceholderValuesIsNotGiven_ShouldCorrectlyInitializeTheInstanceWithANullPlaceholderValues()
    {
        //--- Arrange ---
        var code = "some.code";
        var debugMessageTemplate = "msg";

        //--- Act ---
        var error = new Error(code, debugMessageTemplate);

        //--- Assert ---
        Assert.Equal(code, error.Code);
        Assert.Equal(debugMessageTemplate, error.DebugMessageTemplate);
        Assert.Equal(debugMessageTemplate, error.DebugMessage);
        Assert.Empty(error.PlaceholderValues!);
        Assert.Equal(nameof(Error), error.Type);
        Assert.Equal($"{nameof(Error)} '{code}'", error.Kind);
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
        var error = new Error(applicationUseCaseErrorCode, businessRuleViolationError);

        //--- Assert ---
        var expectedDebugMessage = string.Format(debugMessageTemplate, placeholderValues.ToArray()) + $" (from {businessRuleViolationError.Type})";
        Assert.Equal(applicationUseCaseErrorCode, error.Code);
        Assert.Equal(expectedDebugMessage, error.DebugMessageTemplate);
        Assert.Equal(expectedDebugMessage, error.DebugMessage);
        Assert.Empty(error.PlaceholderValues!);
        Assert.Equal(nameof(Error), error.Type);
        Assert.Equal($"{nameof(Error)} '{applicationUseCaseErrorCode}'", error.Kind);
    }

    [Fact]
    public void Instanciation_WhenAnErrorAndACodeAreGiven_ShouldCorrectlyInitializeTheInstance_2()
    {
        //--- Arrange---
        Fixtures.CustomerNameLengthError customerNameLengthError = new(minLength: 2, maxLength: 70);
        var applicationUseCaseErrorCode = Fixtures.ApplicationCreateCustomerUseCase_NameLengthError.ERROR_CODE;

        //--- Act ---
        var errorForWebApi = new Error(applicationUseCaseErrorCode, customerNameLengthError);

        //--- Assert ---
        var expectedDebugMessage = customerNameLengthError.DebugMessage + $" (from {customerNameLengthError.Type})";
        Assert.Equal(applicationUseCaseErrorCode, errorForWebApi.Code);
        Assert.Equal(expectedDebugMessage, errorForWebApi.DebugMessageTemplate);
        Assert.Equal(expectedDebugMessage, errorForWebApi.DebugMessage);
        Assert.Empty(errorForWebApi.PlaceholderValues!);
        Assert.Equal(nameof(Error), errorForWebApi.Type);
        Assert.Equal($"{nameof(Error)} '{applicationUseCaseErrorCode}'", errorForWebApi.Kind);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void Instanciation_WhenCodeIsEmpty_ShouldThrowAnErrorCodeIsRequiredExceptionWithAppropriateMessage(string emptyCode)
    {
        //--- Act && Assert ---
        var ex = Assert.Throws<ErrorCodeIsRequiredException>(() => new Error(emptyCode, "someMsg {0}", new[] { "someData" }));

        var expectedMessage = string.Format(ErrorCodeIsRequiredException.MESSAGE);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void Instanciation_WhenCodeIsEmpty_ShouldThrowAnErrorCodeIsRequiredExceptionWithAppropriateMessage2(string emptyCode)
    {
        //--- Act && Assert ---
        var ex = Assert.Throws<ErrorCodeIsRequiredException>(() => new Error(emptyCode, "someMsg"));

        var expectedMessage = string.Format(ErrorCodeIsRequiredException.MESSAGE);
        Assert.Equal(expectedMessage, ex.Message);
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
        var error = new Error(errorCode, debugMessageTemplate, placeholderValues);

        //--- Assert ---
        var expected = $"{nameof(Error)} '{errorCode}' : {error.DebugMessage}";
        Assert.Equal(expected, error.ToString());
    }

    [Fact]
    public void ToString_WhenCodeIsGivenAndDebugMessageTemplateIsEmptyOrNotGiven_ShouldReturnTheCorrectValue()
    {
        //--- Arrange ---
        var errorCode = "some.code";
        var debugMessageTemplate = string.Empty;

        //--- Act ---
        var error1 = new Error(errorCode, debugMessageTemplate);
        var error2 = new Error(errorCode);

        //--- Assert ---
        var expected = $"{nameof(Error)} '{errorCode}'";
        Assert.Equal(expected, error1.ToString());
        Assert.Equal(expected, error2.ToString());
    }

    [Fact]
    public void ToString_WhenOnlyCodeAndDebugMessageTemplateAreGiven_ShouldReturnTheCorrectValue()
    {
        //--- Arrange ---
        var errorCode = "some.code";
        var debugMessageTemplate = "SomeMsg";

        //--- Act ---
        var error = new Error(errorCode, debugMessageTemplate);

        //--- Assert ---
        var expected = $"{nameof(Error)} '{errorCode}' : {debugMessageTemplate}";
        Assert.Equal(expected, error.ToString());
    }
    #endregion ToString

}

static partial class Fixtures
{
    internal record CustomerNameLengthError : BusinessRuleViolationError
    {
        public CustomerNameLengthError(int minLength, int maxLength)
            : base("Customer name length must be between {0} and {1}.", new[] { $"{minLength}", $"{maxLength}" })
        {
        }
    }
    internal record ApplicationCreateCustomerUseCase_NameLengthError : Error
    {
        internal const string ERROR_CODE = "x.error";
        public ApplicationCreateCustomerUseCase_NameLengthError(CustomerNameLengthError businessRuleViolationError)
            : base(ERROR_CODE, businessRuleViolationError)
        {

        }
    }
}