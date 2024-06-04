using Xunit;


using General.Basics.ErrorHandling;

using General.Basics.Extensions;


namespace General.Basics.Extensions.UnitTests;

public class IEnumerableExtensionTests
{
    #region GetNearestInfValue_
    [Theory]
    [ClassData(typeof(GetNearestInfValueData))]
    public void GetNearestInfValue_WhenNearestInfValueExist_ShouldReturnThisNearestInfValue(int searchedValue, int expectedNearestValue)
    {
        var list = new List<int> { 15, 0, 25, 15, 10, 6, 0, 25, 13, 29, 32, 11 };

        int? nearestInfValue = list.GetNearestInfValue_(searchedValue);
        Assert.Equal(expectedNearestValue, nearestInfValue);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(4)]
    [InlineData(0)]
    [InlineData(-1)]
    public void GetNearestInfValue_WhenNearestInfValueDoesNotExist_ShouldReturnNull(int searchedValue)
    {
        var list = new List<int> { 15, 6, 25 };

        int? nearestInfValue = list.GetNearestInfValue_(searchedValue);

        Assert.Null(nearestInfValue);
    }
    #endregion GetNearestInfValue_


    #region GetLastIndex_
    [Fact]
    public void GetLastIndex_WhenEnumerableIsEmpty_ShouldReturnNull()
    {
        var list = new List<int>() { };
        Assert.Null(list.GetLastIndex_());
    }

    [Fact]
    public void GetLastIndex_WhenEnumerableIsNotEmpty_ShouldReturnTheCorrectIndex()
    {
        var list = new List<int>() { 1, 2, 3 };
        var lastIndex = list.Count - 1;
        Assert.Equal(lastIndex, list.GetLastIndex_());
    }
    #endregion GetLastIndex_

    #region IsEmpty_
    [Fact]
    public void IsEmpty_WhenNoElements_ShouldReturnTrue()
    {
        //--- Arrange ---
        int[] ints = new int[] { };
        List<bool> bools = new() { };


        //--- Act ---
        var result1 = ints.IsEmpty_<int>();
        var result2 = bools.IsEmpty_<bool>();

        //--- Assert ---
        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact]
    public void IsEmpty_WhenAtLeast1Element_ShouldReturnFalse()
    {
        //--- Arrange ---
        int[] ints = new int[] { 0 };
        List<string> strings = new() { "" };


        //--- Act ---
        var result1 = ints.IsEmpty_<int>();
        var result2 = strings.IsEmpty_<string>();

        //--- Assert ---
        Assert.False(result1);
        Assert.False(result2);
    }
    #endregion IsEmpty_


    #region IsValidIndex_
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    public void IsValidIndex_WhenEnumerableIsEmpty_ShouldReturnFalse(int index)
    {
        var list = new List<int>() { };
        var result = list.IsValidIndex_(index);

        Assert.False(result);
    }

    [Fact]
    public void IsValidIndex_WhenIsInvalidIndexOnList_ShouldReturnFalse()
    {
        var list = new List<int>() { 1, 2, 3 };
        var maxIndex = list.Count - 1;
        var invalidIndex = maxIndex + 1;
        var result = list.IsValidIndex_(invalidIndex);

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
        var maxIndex = list.Count - 1;
        var result = list.IsValidIndex_(maxIndex);

        Assert.True(result);
    }

    [Fact]
    public void IsValidIndex_WhenIsInvalidIndexOnArray_ShouldReturnFalse()
    {
        int[] array = { 1, 2, 3 };
        var maxIndex = array.Length - 1;
        var invalidIndex = maxIndex + 1;
        var result = array.IsValidIndex_(invalidIndex);

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
        var maxIndex = array.Length - 1;
        var result = array.IsValidIndex_(maxIndex);

        Assert.True(result);
    }
    #endregion IsValidIndex_

    #region CheckIsValidIndex_
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    public void CheckIsValidIndex_WhenEnumerableIsEmpty_ShouldThrowAnUnexistingIndexBecauseEmptyException(int index)
    {
        var list = new List<int>() { };

        var ex = Assert.Throws<UnexistingIndexBecauseEmptyException>(() => list.CheckIsValidIndex_(index));

        var expectedMessage = string.Format(UnexistingIndexBecauseEmptyException.MESSAGE_FORMAT, "IEnumerable", index);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void CheckIsValidIndex_WhenIsValidIndex_ShouldNotThrowAnException()
    {
        var list = new List<int>() { 1, 2, 3 };
        var maxIndex = list.Count - 1;
        list.CheckIsValidIndex_(maxIndex);
        Assert.True(true);
    }

    [Fact]
    public void CheckIsValidIndex_WhenIsInvalidIndexAndNotEmptyEnumerable_ShouldThrowAnOutOfRangeIntegerException()
    {
        var list = new List<int>() { 1, 2, 3 };
        var maxIndex = list.Count - 1;
        var invalidIndex = maxIndex + 1;
        var ex = Assert.Throws<OutOfRangeIntegerException>(() => list.CheckIsValidIndex_(invalidIndex));

        var expectedMessage = string.Format(OutOfRangeIntegerException.MESSAGE_FORMAT, "IEnumerable Index", invalidIndex, 0, list.Count - 1);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion CheckIsValidIndex_


    #region ChunkExists_
    [Theory]
    [InlineData(-1, -1)]
    [InlineData(-1, 0)]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    public void ChunkExists_WhenEnumerableIsEmpty_ShouldReturnFalse(int startIndex, int endIndex)
    {
        var list = new List<int>() { };

        var result = list.ChunkExists_(startIndex, endIndex);

        Assert.False(result);
    }

    [Theory]
    [ClassData(typeof(UnexistingChunkBoundsData))]
    public void ChunkExists_WhenChunkDoesntExist_ShouldReturnFalse(int startIndex, int endIndex)
    {
        var list = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };

        var result = list.ChunkExists_(startIndex, endIndex);

        Assert.False(result);
    }

    [Theory]
    [ClassData(typeof(ExistingChunkBoundsData))]
    public void ChunkExists_WhenChunkExists_ShouldReturnTrue(int startIndex, int endIndex)
    {
        var list = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };

        var result = list.ChunkExists_(startIndex, endIndex);

        Assert.True(result);
    }
    #endregion ChunkExists_

    #region CheckChunkExists_
    [Theory]
    [InlineData(-1, -1)]
    [InlineData(-1, 0)]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    public void ChunkExists_WhenEnumerableIsEmpty_ShouldThrowAnUnexistingChunkBecauseEmptyException(int startIndex, int endIndex)
    {
        var list = new List<int>() { };

        var ex = Assert.Throws<UnexistingChunkBecauseEmptyException>(() => list.CheckChunkExists_(startIndex, endIndex));

        var expectedMessage = string.Format(UnexistingChunkBecauseEmptyException.MESSAGE_FORMAT, "IEnumerable", startIndex, endIndex);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Theory]
    [ClassData(typeof(ExistingChunkBoundsData))]
    public void CheckChunkExists_WhenChunkExists_ShouldNotThrowAnException(int startIndex, int endIndex)
    {
        var list = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };

        list.CheckChunkExists_(startIndex, endIndex);

        Assert.True(true);
    }

    [Theory]
    [ClassData(typeof(UnexistingChunkBoundsData))]
    public void CheckChunkExists_WhenChunkDoesntExist_ShouldThrowAnUnexistingChunkException(int startIndex, int endIndex)
    {
        var list = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
        var minIndex = 0;
        var maxIndex = list.Count - 1;

        var ex = Assert.Throws<UnexistingChunkException>(() => list.CheckChunkExists_(startIndex, endIndex));

        var expectedMessage = string.Format(UnexistingChunkException.MESSAGE_FORMAT, "IEnumerable", startIndex, endIndex, minIndex, maxIndex);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion CheckChunkExists_

    #region GetChunk_
    [Theory]
    [InlineData(-1, -1)]
    [InlineData(-1, 0)]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    public void GetChunk_WhenEnumerableIsEmpty_ShouldThrowAnUnexistingChunkBecauseEmptyException(int startIndex, int endIndex)
    {
        List<int> list = new();

        var ex = Assert.Throws<UnexistingChunkBecauseEmptyException>(() => list.GetChunk_(startIndex, endIndex));

        var expectedMessage = string.Format(UnexistingChunkBecauseEmptyException.MESSAGE_FORMAT, "IEnumerable", startIndex, endIndex);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Theory]
    [ClassData(typeof(ExistingChunkBoundsDataWithResult))]
    public void GetChunk_WhenChunkExists_ShouldReturnTheCorrectChunk(int startIndex, int endIndex, IEnumerable<int> expectedChunk)
    {
        var list = new List<int>() { 0, 1, 2, 3, 4, 5, 6 }; ;

        var result = list.GetChunk_(startIndex, endIndex);

        Assert.Equal(expectedChunk, result);
    }

    [Theory]
    [ClassData(typeof(UnexistingChunkBoundsData))]
    public void GetChunk_WhenChunkDoesntExistAndEnumerableNotEmpty_ShouldThrowAnUnexistingChunkException(int startIndex, int endIndex)
    {
        var list = new List<int>() { 0, 1, 2, 3, 4, 5, 6 }; ;
        var minIndex = 0;
        var maxIndex = list.Count - 1;

        var ex = Assert.Throws<UnexistingChunkException>(() => list.GetChunk_(startIndex, endIndex));

        var expectedMessage = string.Format(UnexistingChunkException.MESSAGE_FORMAT, "IEnumerable", startIndex, endIndex, minIndex, maxIndex);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion GetChunk_


    //---------------------------------------------------------
    class UnexistingChunkBoundsData : TheoryData<int, int>
    {
        public UnexistingChunkBoundsData()
        {
            Add(-1, 0);
            Add(1, 7);
            Add(7, 8);
            Add(3, 1);
            Add(1, 0);
        }
    }
    class ExistingChunkBoundsData : TheoryData<int, int>
    {
        public ExistingChunkBoundsData()
        {
            Add(0, 0);
            Add(0, 1);
            Add(0, 6);
            Add(1, 6);
            Add(3, 5);
            Add(6, 6);
        }
    }
    class ExistingChunkBoundsDataWithResult : TheoryData<int, int, IEnumerable<int>>
    {
        public ExistingChunkBoundsDataWithResult()
        {
            Add(0, 0, new[] { 0 });
            Add(0, 1, new[] { 0, 1 });
            Add(0, 6, new[] { 0, 1, 2, 3, 4, 5, 6 });
            Add(1, 6, new[] { 1, 2, 3, 4, 5, 6 });
            Add(3, 5, new[] { 3, 4, 5 });
            Add(6, 6, new[] { 6 });
        }
    }

    class GetNearestInfValueData : TheoryData<int, int>
    {
        public GetNearestInfValueData()
        {
            Add(14, 13);
            Add(15, 15);
            Add(29, 29);
            Add(31, 29);
            Add(900, 32);
            Add(24, 15);
            Add(12, 11);
            Add(11, 11);
            Add(9, 6);
            Add(5, 0);
            Add(28, 25);
            Add(32, 32);
            Add(21, 15);
        }
    }
}
