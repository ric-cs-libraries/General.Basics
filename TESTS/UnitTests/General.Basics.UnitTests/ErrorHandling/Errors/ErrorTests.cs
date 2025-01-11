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
        var debugMessage = string.Format(debugMessageTemplate, placeholderValues!.ToArray());

        //--- Act ---
        var error = new Error(code, debugMessageTemplate, placeholderValues);

        //--- Assert ---
        Assert.Equal(code, error.Code);
        Assert.Equal(debugMessageTemplate, error.DebugMessageTemplate);
        Assert.Equal(placeholderValues, error.PlaceholderValues);
        Assert.Equal(debugMessage, error.DebugMessage);
        Assert.Equal(nameof(Error), error.Type);
    }

    [Fact]
    public void Instanciation_WhenPlaceholderValuesIsNotGiven_ShouldCorrectlyInitializeTheInstanceWithANullPlaceholderValues()
    {
        //--- Act ---
        var code = "some.code";
        var debugMessageTemplate = "msg";
        var error = new Error(code, debugMessageTemplate);

        //--- Assert ---
        Assert.Equal(code, error.Code);
        Assert.Equal(debugMessageTemplate, error.DebugMessageTemplate);
        Assert.Empty(error.PlaceholderValues!);
        Assert.Equal(debugMessageTemplate, error.DebugMessage);
        Assert.Equal(nameof(Error), error.Type);
    }

    [Fact]
    public void Instanciation_WhenOnlyErrorCodeIsGiven_ShouldCorrectlyInitializeTheInstance()
    {
        //--- Act ---
        var code = "some.code";
        var error = new Error(code);

        //--- Assert ---
        Assert.Equal(code, error.Code);
        Assert.Empty(error.PlaceholderValues!);
        Assert.Empty(error.DebugMessageTemplate);
        Assert.Empty(error.DebugMessage);
        Assert.Equal(nameof(Error), error.Type);
    }

    [Fact]
    public void Instanciation_WhenErrorCodeIsEmptyStringOrWhiteSpacesOnly_ShouldThrowAnErrorCodeIsRequiredException()
    {
        //--- Act & Assert ---
        Assert.Throws<ErrorCodeIsRequiredException>(() => new Error(" ", "msg"));
        Assert.Throws<ErrorCodeIsRequiredException>(() => new Error("", "msg"));
    }

    [Fact]
    public void ToString_WhenAllInfosHaveBeenGiven_ShouldReturnTheCorrectValue()
    {
        //--- Act ---
        var errorCode = "some.code";
        var debugMessageTemplate = "myDebugMessageTemplate : {0} and {1}";
        var placeholderValues = new[] { "txt1", "txt2" };
        var error = new Error(errorCode, debugMessageTemplate, placeholderValues);

        //--- Assert ---
        var expected = $"Error '{errorCode}' : {error.DebugMessage}";
        Assert.Equal(expected, error.ToString());
    }

    [Fact]
    public void ToString_WhenOnlyErrorCodeIsGiven_ShouldReturnTheCorrectValue()
    {
        //--- Act ---
        var errorCode = "some.code";
        var error = new Error(errorCode);

        //--- Assert ---
        var expected = $"Error '{errorCode}'";
        Assert.Equal(expected, error.ToString());
    }

}
