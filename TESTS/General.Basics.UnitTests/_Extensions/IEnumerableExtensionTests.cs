﻿using Xunit;


using General.Basics.ErrorHandling;

using General.Basics.Extensions;
using General.Basics.Generators.Extensions;

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

        var expectedMessage = string.Format(UnexistingIndexBecauseEmptyException.MESSAGE_FORMAT, "List`1", index);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void CheckIsValidIndex_WhenIndexIsValid_ShouldNotThrowAnException()
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

        var expectedMessage = string.Format(OutOfRangeIntegerException.MESSAGE_FORMAT, "List`1 Index", invalidIndex, 0, maxIndex);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion CheckIsValidIndex_

    #region CheckAreValidIndexes_
    [Fact]
    public void CheckAreValidIndexes_WhenEnumerableIsEmpty_ShouldThrowAnUnexistingIndexBecauseEmptyException()
    {
        var list = new List<int>() { };

        var ex = Assert.Throws<UnexistingIndexBecauseEmptyException>(() => list.CheckAreValidIndexes_(new[] {1,2,3}));
    }
    [Fact]
    public void CheckAreValidIndexes_WhenIndexesAreValid_ShouldNotThrowAnException()
    {
        var list = new List<int>() { 1, 2, 3 };
        list.CheckAreValidIndexes_(new[] { 0, 1, 2 });
        Assert.True(true);
    }
    [Fact]
    public void CheckAreValidIndexes_IfAnIndexIsInvalidAndNotEmptyEnumerable_ShouldThrowAnOutOfRangeIntegerException()
    {
        var list = new List<int>() { 1, 2, 3 };
        var maxIndex = list.Count - 1;
        var invalidIndex = maxIndex + 1;
        var ex = Assert.Throws<OutOfRangeIntegerException>(() => list.CheckAreValidIndexes_(new[] { 0, 1, 2, invalidIndex }));

        var expectedMessage = string.Format(OutOfRangeIntegerException.MESSAGE_FORMAT, "List`1 Index", invalidIndex, 0, maxIndex);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion CheckAreValidIndexes_


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

        var expectedMessage = string.Format(UnexistingChunkBecauseEmptyException.MESSAGE_FORMAT, "List`1", startIndex, endIndex);
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

        var expectedMessage = string.Format(UnexistingChunkException.MESSAGE_FORMAT, "List`1", startIndex, endIndex, minIndex, maxIndex);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion CheckChunkExists_

    #region GetChunk_ (with startIndex and endIndex specified)
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

        var expectedMessage = string.Format(UnexistingChunkBecauseEmptyException.MESSAGE_FORMAT, "List`1", startIndex, endIndex);
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

        var expectedMessage = string.Format(UnexistingChunkException.MESSAGE_FORMAT, "List`1", startIndex, endIndex, minIndex, maxIndex);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion GetChunk_ (with startIndex and endIndex specified)

    #region GetChunk_ (with only startIndex specified)
    [Fact]
    public void GetChunk_WhenChunkExists_ShouldReturnTheCorrectChunk_()
    {
        var list = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
        var startIndex = 3;

        var result = list.GetChunk_(startIndex);

        Assert.Equal(new[] {3,4,5,6}, result);
    }
    [Fact]
    public void GetChunk_WhenChunkExistsAndStartIndexEqualsLastIndex_ShouldReturnTheCorrectChunkWithOnlyTheLastValue_()
    {
        var list = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
        var startIndex = list.GetLastIndex_();

        var result = list.GetChunk_(startIndex!.Value);

        Assert.Equal(new[] { 6 }, result);
    }
    [Fact]
    public void GetChunk_WhenChunkDoesntExist_ShouldThrowAnUnexistingChunkException()
    {
        var list = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
        var minIndex = 0;
        var maxIndex = list.GetLastIndex_();
        var startIndex = maxIndex!.Value + 1;
        int endIndex = maxIndex!.Value;

        var ex = Assert.Throws<UnexistingChunkException>(() => list.GetChunk_(startIndex));

        var expectedMessage = string.Format(UnexistingChunkException.MESSAGE_FORMAT, "List`1", startIndex, endIndex, minIndex, maxIndex);
        Assert.Equal(expectedMessage, ex.Message);
    }
    [Fact]
    public void GetChunk_WhenEnumerableIsEmpty_ShouldThrowAnUnexistingChunkBecauseEmptyException_()
    {
        List<int> list = new();
        var startIndex = 0;
        int? endIndex = null;

        var ex = Assert.Throws<UnexistingChunkBecauseEmptyException>(() => list.GetChunk_(startIndex));

        var expectedMessage = string.Format(UnexistingChunkBecauseEmptyException.MESSAGE_FORMAT, "List`1", startIndex, endIndex);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion GetChunk_ (with only startIndex specified)


    #region GetChunkFromEnd_
    [Fact]
    public void GetChunkFromEnd_WhenFullChunkExists_ShouldReturnTheCorrectChunk()
    {
        var list = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
        var chunkLength = 4;

        var result = list.GetChunkFromEnd_(chunkLength);

        Assert.Equal(new[] { 3, 4, 5, 6 }, result);
    }
    [Fact]
    public void GetChunkFromEnd_WhenFullChunkExists_ShouldReturnTheCorrectChunk_()
    {
        var str = "ABCDEF"; //IEnumerable<char>
        var chunkLength = 4;

        var result = str.GetChunkFromEnd_(chunkLength); //Mais ici result n'est pas une string, mais une IEnumerable<char> .

        Assert.Equal("CDEF".ToCharArray(), result);
    }
    [Fact]
    public void GetChunkFromEnd_WhenFullChunkExistsAndChunkLengthIsEqualToTheEnumerableLength_ShouldReturnAnEnumerableContainingAllTheElements()
    {
        var list = new List<int>() { 0, 1, 2, 3, 4 };
        var chunkLength = list.Count;

        var result = list.GetChunkFromEnd_(chunkLength);

        Assert.Equal(list, result);
        Assert.False(list == result);
    }
    
    [Fact]
    public void GetChunkFromEnd_WhenChunkLengthIsLongerThanTheEnumerableLength_ShouldThrowAnUnexistingChunkException()
    {
        var list = new List<int>() { 0, 1, 2, 3, 4 };
        var chunkLength = list.Count + 1;
        var minIndex = 0;
        var maxIndex = list.GetLastIndex_();
        int endIndex = maxIndex!.Value;
        int startIndex = endIndex - (chunkLength-1);

        var ex = Assert.Throws<UnexistingChunkException>(() => list.GetChunkFromEnd_(chunkLength));

        var expectedMessage = string.Format(UnexistingChunkException.MESSAGE_FORMAT, "List`1", startIndex, endIndex, minIndex, maxIndex);
        Assert.Equal(expectedMessage, ex.Message);
    }
    //[Theory]
    //[InlineData(0)]
    //[InlineData(1)]
    //public void GetChunkFromEnd_WhenEnumerableIsEmpty_ShouldAlwaysThrowAnUnexistingChunkBecauseEmptyException(int chunkLength)
    //{
    //    List<int> list = new();
    //    int? startIndex = null;
    //    int? endIndex = null;

    //    var ex = Assert.Throws<UnexistingChunkBecauseEmptyException>(() => list.GetChunkFromEnd_(chunkLength));

    //    var expectedMessage = string.Format(UnexistingChunkBecauseEmptyException.MESSAGE_FORMAT, "List`1", startIndex, endIndex);
    //    Assert.Equal(expectedMessage, ex.Message);
    //}

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void GetChunkFromEnd_WhenEnumerableIsEmptyAndChunkLengthIsNotNegative_ShouldAlwaysReturnAnEmptyEnumerable(int chunkLength)
    {
        var list = new List<int>() { };

        var result = list.GetChunkFromEnd_(chunkLength);

        Assert.Empty(result);
    }

    [Fact]
    public void GetChunkFromEnd_WhenChunkLengthIsZero_ShouldAlwaysReturnAnEmptyEnumerable()
    {
        var chunkLength = 0;
        var list1 = new List<int>() { 0, 1, 2, 3, 4 };
        var list2 = new List<int>() {};

        var result1 = list1.GetChunkFromEnd_(chunkLength);
        var result2 = list2.GetChunkFromEnd_(chunkLength);

        Assert.Empty(result1);
        Assert.Empty(result2);
    }

    [Fact]
    public void GetChunkFromEnd_WhenChunkLengthIsNegative_ShouldAlwaysThrowMustBePositiveIntegerException()
    {
        var list1 = new List<int>() { 0, 1, 2, 3, 4 };
        var list2 = new List<int>() { };
        var chunkLength = -1;

        var ex1 = Assert.Throws<MustBePositiveIntegerException>(() => list1.GetChunkFromEnd_(chunkLength));
        var ex2 = Assert.Throws<MustBePositiveIntegerException>(() => list2.GetChunkFromEnd_(chunkLength));

        var expectedMessage1 = string.Format(MustBePositiveIntegerException.MESSAGE_FORMAT, "Chunk length", chunkLength);
        var expectedMessage2 = expectedMessage1;
        Assert.Equal(expectedMessage1, ex1.Message);
        Assert.Equal(expectedMessage2, ex2.Message);
    }
    #endregion GetChunkFromEnd_



    #region ToChunks_
    [Fact]
    public void ToChunks_WhenNbElementsAllowsAllChunksToHaveTheIdealSize_ShouldOnlyReturnChunksOfTheSameSize()
    {
        var list = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

        int idealNbElementsInAChunk = 3;

        //
        IList<IEnumerable<int>> result = list.ToChunks_(idealNbElementsInAChunk);

        //
        List<IEnumerable<int>> expectedResult = new()
        {
            new int[] { 0, 1, 2 },
            new int[] { 3, 4, 5 },
            new int[] { 6, 7, 8 },
            new int[] { 9, 10, 11 },
            new int[] { 12, 13, 14 },
            new int[] { 15, 16, 17 },
            new int[] { 18, 19, 20 }
        };
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void ToChunks_WhenNbElementsDoesntAllowAllChunksToHaveTheIdealSize_ShouldReturnChunksOfTheSameSizeExceptTheLastOne()
    {
        var list = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 };

        int idealNbElementsInAChunk = 3;

        //
        IList<IEnumerable<int>> result = list.ToChunks_(idealNbElementsInAChunk);

        //
        List<IEnumerable<int>> expectedResult = new()
        {
            new int[] { 0, 1, 2 },
            new int[] { 3, 4, 5 },
            new int[] { 6, 7, 8 },
            new int[] { 9, 10, 11 },
            new int[] { 12, 13, 14 },
            new int[] { 15, 16, 17 },
            new int[] { 18, 19, 20 },
            new int[] { 21, 22 },
        };
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void ToChunks_WhenNbElementsDoesntAllowAllChunksToHaveTheIdealSize_ShouldReturnChunksOfTheSameSizeExceptTheLastOne_2()
    {
        string str = "ABCDEFGHIJKLMN";

        int idealNbElementsInAChunk = 3;

        //
        IList<IEnumerable<char>> result = str.ToChunks_(idealNbElementsInAChunk);

        //
        List<IEnumerable<char>> expectedResult = new()
        {
            new char[] { 'A', 'B', 'C' },
            new char[] { 'D', 'E', 'F' },
            new char[] { 'G', 'H', 'I' },
            new char[] { 'J', 'K', 'L' },
            new char[] { 'M', 'N' },
        };
        Assert.Equal(expectedResult, result);
        //Assert.Equal("ABC", string.Join("", expectedResult[0]));
        //Assert.Equal("ABC", expectedResult[0].ToString_());
    }

    [Fact]
    public void ToChunks_WhenListIsEmpty_ShouldReturnAnEmptyList()
    {
        var list = new List<int>() {};

        //
        IList<IEnumerable<int>> result = list.ToChunks_(1);

        //
        Assert.Empty(result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ToChunks_WhenParamIdealNbElementsInAChunkIsUnder1_ShouldThrowAnIntShouldBeGreaterOrEqualException(int invalidIdealNbElementsInAChunk)
    {
        var list = new List<int>() { 10, 20, 30, 40};

        //Act & Assert
        var ex = Assert.Throws<IntShouldBeGreaterOrEqualException>(() => list.ToChunks_(invalidIdealNbElementsInAChunk));

        var expectedMessage = string.Format(IntShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "idealNbElementsInAChunk", invalidIdealNbElementsInAChunk, 1);
        Assert.Equal(expectedMessage, ex.Message);

    }
    #endregion ToChunks_


    #region ToString_
    [Fact]
    public void ToString_WhenTypeIsString_ShouldReturnTheCorrectConcatenatedString()
    {
        var list = new List<string>() { "HOW", " ", "ARE", " ", "YOU ?"};

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
        var chars1 = new List<char>() { 'A', 'B'};
        var chars2 = new List<char>() { 'C', 'D'};
        var chars3 = new List<char>() { 'I' };
        IEnumerable<IEnumerable<char>> list = new[] { chars1, chars2, chars3 };

        //
        string result = list.ToString_();

        //
        Assert.Equal($"{chars1[0]}{chars1[1]}{chars2[0]}{chars2[1]}{chars3[0]}", result);
    }
    #endregion ToString_


    //=============================================================================================
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
