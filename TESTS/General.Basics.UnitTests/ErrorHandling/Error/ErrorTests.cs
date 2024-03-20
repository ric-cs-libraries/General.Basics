using Xunit;


using General.Basics.ErrorHandling;


namespace General.Basics.ErrorHandling.UnitTests;

public class ErrorTests
{
    [Theory]
    [InlineData("some.error")]
    [InlineData("some.error", "msg for dev")]
    [InlineData("some.error", "msg for dev", "field1")]
    public void Instanciation__ShouldCorrectlyInitializeTheInstance(string code, string debugMessage = "", string relatedFieldName = "")
    {
        //--- Act ---
        var error = new Error(code, debugMessage, relatedFieldName);

        //--- Assert ---
        Assert.Equal(code, error.Code);
        Assert.Equal(debugMessage, error.DebugMessage);
        Assert.Equal(relatedFieldName, error.RelatedFieldName);
    }

    [Fact]
    public void Instanciation_WhenRelatedFieldNameIsNotPassed_ShouldCorrectlyInitializeTheInstanceWithAnEmptyStringRelatedFieldName()
    {
        //--- Act ---
        var error = new Error("some.code", "msg");

        //--- Assert ---
        Assert.Empty(error.RelatedFieldName);
    }

    [Fact]
    public void Instanciation_WhenDebugMessageIsNotPassed_ShouldCorrectlyInitializeTheInstanceWithAnEmptyStringDebugMessage()
    {
        //--- Act ---
        var error = new Error("some.code");

        //--- Assert ---
        Assert.Empty(error.DebugMessage);
    }

    [Fact]
    public void Instanciation_WhenErrorCodeIsEmptyStringOrWhiteSpacesOnly_ShouldThrowAnErrorCodeIsRequiredException()
    {
        //--- Act & Assert ---
        Assert.Throws<ErrorCodeIsRequiredException>(() => new Error(" ", "msg", "field1"));
        Assert.Throws<ErrorCodeIsRequiredException>(() => new Error("", "msg", "field1"));
    }

    [Theory]
    [InlineData("some.error")]
    [InlineData("some.error", "msg for dev")]
    [InlineData("some.error", "msg for dev", "field1")]
    public void ImplicitOperator__ShouldReturnErrorToString(string code, string debugMessage = "", string relatedFieldName = "")
    {
        //--- Act ---
        Error error = new(code, debugMessage, relatedFieldName);

        //--- Assert ---
        Assert.Equal(error.ToString(), error);
    }

    [Fact]
    public void ToString_WhenAllInfosHaveBeenGiven_ShouldReturnTheCorrectValue()
    {
        //--- Act ---
        var errorCode = "some.code";
        var debugMessage = "myDebugMessage";
        var relatedFieldName = "myRelatedFieldName";
        var error = new Error(errorCode, debugMessage, relatedFieldName);

        //--- Assert ---
        var expected = $"Error '{errorCode}' : {debugMessage} (relatedFieldName : '{relatedFieldName}')";
        Assert.Equal(expected, error.ToString());
    }

    [Fact]
    public void ToString_WhenRelatedFieldNameIsNotGiven_ShouldReturnTheCorrectValue()
    {
        //--- Act ---
        var errorCode = "some.code";
        var debugMessage = "myDebugMessage";
        var error = new Error(errorCode, debugMessage);

        //--- Assert ---
        var expected = $"Error '{errorCode}' : {debugMessage}";
        Assert.Equal(expected, error.ToString());
    }

    [Fact]
    public void ToString_WhenOnlyTheErrorCodeIsGiven_ShouldReturnTheCorrectValue()
    {
        //--- Act ---
        var errorCode = "some.code";
        var error = new Error(errorCode);

        //--- Assert ---
        var expected = $"Error '{errorCode}'";
        Assert.Equal(expected, error.ToString());
    }
}
