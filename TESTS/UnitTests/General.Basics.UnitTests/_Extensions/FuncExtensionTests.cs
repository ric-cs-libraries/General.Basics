using General.Basics.ErrorHandling;
using General.Basics.Extensions;
using Xunit;


namespace General.Basics.UnitTests.Extensions.Numbers;

public class FuncExtensionTests
{
    #region Recurse<T>
    [Theory]
    [InlineData(1, 10, 1)]
    [InlineData(2, 1, 4)]
    [InlineData(2, 2, 16)]
    [InlineData(2, 3, 256)]
    [InlineData(10, 1, 100)]
    [InlineData(10, 2, 10_000)]
    [InlineData(10, 3, 100_000_000)]
    [InlineData(-1, 10, 1)]
    [InlineData(-2, 1, 4)]
    [InlineData(-2, 2, 16)]
    [InlineData(-2, 3, 256)]
    [InlineData(-10, 1, 100)]
    [InlineData(-10, 2, 10_000)]
    [InlineData(-10, 3, 100_000_000)]
    public void Recurse_WhenNbRecurseIsValid_ShouldReturnTheCorrectValue(int firstCallParam, int nbRecurse, int expectedResult)
    {
        //
        Func<int, int> square = (int i) => (int)Math.Pow((double)i, 2);

        //
        int result = square.Recurse<int>(firstCallParam, nbRecurse);

        //
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData("a", 1, "*a*")]
    [InlineData("a", 2, "**a**")]
    [InlineData("a", 3, "***a***")]
    public void Recurse_WhenNbRecurseIsValid_ShouldReturnTheCorrectValue_2(string firstCallParam, int nbRecurse, string expectedResult)
    {
        //
        Func<string, string> starQuote = (string str) => $"*{str}*";

        //
        string result = starQuote.Recurse<string>(firstCallParam, nbRecurse);

        //
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    public void Recurse_WhenNbRecurseIsInvalid_ShouldThrowAnIntShouldBeGreaterOrEqualExceptionWithCorrectMessage(int nbRecurse)
    {
        //
        Func<string, string> starQuote = (string str) => $"*{str}*";

        //
        var ex = Assert.Throws<IntShouldBeGreaterOrEqualException>(() => starQuote.Recurse<string>("anyParam", nbRecurse));

        var expectedMessage = string.Format(IntShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "nbRecurse", nbRecurse, 1);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion Recurse<T>

}
