using Xunit;


using Transverse._Common.General.Basics.Extensions;
using Transverse._Common.General.Basics.Exceptions;


namespace Transverse._Common.General.Basics.Extensions.IEnumerable.UnitTests;

public class IEnumerableExtensionTests
{
    #region IsValidIndex_
    [Fact]
    public void IsValidIndex_WhenIsInvalidIndexOnList_ShouldReturnFalse()
    {
        var list = new List<int>() { 1, 2, 3 };

        var result = list.IsValidIndex_(list.Count);

        Assert.False(result);
    }

    [Fact]
    public void IsValidIndex_WhenIsInvalidIndexOnList_ShouldReturnFalse_2()
    {
        var list = new List<int>() { 1, 2, 3 };

        var result = list.IsValidIndex_(-1);

        Assert.False(result);
    }

    [Fact]
    public void IsValidIndex_WhenIsValidIndexOnList_ShouldReturnTrueTrue()
    {
        var list = new List<int>() { 1, 2, 3 };

        var result = list.IsValidIndex_(list.Count - 1);

        Assert.True(result);
    }

    [Fact]
    public void IsValidIndex_WhenIsInvalidIndexOnArray_ShouldReturnFalse()
    {
        int[] array = { 1, 2, 3 };

        var result = array.IsValidIndex_(array.Length);

        Assert.False(result);
    }

    [Fact]
    public void IsValidIndex_WhenIsInvalidIndexOnArray_ShouldReturnFalse_2()
    {
        int[] array = { 1, 2, 3 };

        var result = array.IsValidIndex_(-1);

        Assert.False(result);
    }

    [Fact]
    public void IsValidIndex_WhenIsValidIndexOnArray_ShouldReturnTrue()
    {
        int[] array = { 1, 2, 3 };

        var result = array.IsValidIndex_(array.Length - 1);

        Assert.True(result);
    }
    #endregion IsValidIndex_


    #region CheckIsValidIndex_
    [Fact]
    public void CheckIsValidIndex_WhenIsValidIndex_ShouldnotThrowAnException()
    {
        var list = new List<int>() { 1, 2, 3 };
        var validIndex = list.Count - 1;
        list.CheckIsValidIndex_(validIndex);
        Assert.True(true);
    }

    [Fact]
    public void CheckIsValidIndex_WhenIsInvalidIndex_ShouldThrowAnOutOfRangeIntegerException()
    {
        var list = new List<int>() { 1, 2, 3 };
        var invalidIndex = list.Count;
        var ex = Assert.Throws<OutOfRangeIntegerException>(() => list.CheckIsValidIndex_(invalidIndex));

        var expectedMessage = $"Invalid IEnumerable Index : '{invalidIndex}', possible range : [{0},{list.Count-1}].";
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion CheckIsValidIndex_

}
