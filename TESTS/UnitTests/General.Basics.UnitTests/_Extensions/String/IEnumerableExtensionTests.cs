using Xunit;

namespace General.Basics.Extensions.UnitTests;

public partial class IEnumerableExtensionTests
{
    #region ToString_
    [Fact]
    public void ToString_WhenTypeIsString_ShouldReturnTheCorrectConcatenatedString()
    {
        var list = new List<string>() { "HOW", " ", "ARE", " ", "YOU ?" };

        //
        string result = list.ToString_();

        //
        Assert.Equal($"{list[0]}{list[1]}{list[2]}{list[3]}{list[4]}", result);
    }
    [Fact]
    public void ToString_WhenTypeIsChar_ShouldReturnTheCorrectConcatenatedString()
    {
        var list = new List<char>() { 'A', 'B', 'C', 'D' };

        //
        string result = list.ToString_();

        //
        Assert.Equal($"{list[0]}{list[1]}{list[2]}{list[3]}", result);
    }
    [Fact]
    public void ToString_WhenTypeIsIEnumerableOfChars_ShouldReturnTheCorrectConcatenatedString()
    {
        var chars1 = new List<char>() { 'A', 'B' };
        var chars2 = new List<char>() { 'C', 'D' };
        var chars3 = new List<char>() { 'I' };
        IEnumerable<IEnumerable<char>> list = new[] { chars1, chars2, chars3 };

        //
        string result = list.ToString_();

        //
        Assert.Equal($"{chars1[0]}{chars1[1]}{chars2[0]}{chars2[1]}{chars3[0]}", result);
    }
    #endregion ToString_

}
