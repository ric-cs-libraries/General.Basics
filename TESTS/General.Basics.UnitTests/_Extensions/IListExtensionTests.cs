using Xunit;


using General.Basics.ErrorHandling;

using General.Basics.Extensions;


namespace General.Basics.Extensions.UnitTests;

public class IListExtensionTests
{
    #region Swap_
    [Fact]
    public void Swap_WhenIndexAreValidAndDifferent_ShouldSwap()
    {
        //
        var list = new List<int>() { 0, 10, 20, 30, 40, 50, 60 };

        int index = 0;
        int initialValAtIndex = list.ElementAt(index);
        int otherIndex = 6;
        int initialValAtOtherIndex = list.ElementAt(otherIndex);
        var expectedList1 = new List<int>() { initialValAtOtherIndex, 10, 20, 30, 40, 50, initialValAtIndex };
        var expectedList2 = list.ToList(); //Shallow copy


        //
        list.Swap_(index, otherIndex);

        //
        Assert.Equal(list.ElementAt(index), initialValAtOtherIndex);
        Assert.Equal(list.ElementAt(otherIndex), initialValAtIndex);
        Assert.Equal(list, expectedList1);

        list.Swap_(otherIndex, index);
        Assert.Equal(list.ElementAt(index), initialValAtIndex);
        Assert.Equal(list.ElementAt(otherIndex), initialValAtOtherIndex);
        Assert.Equal(list, expectedList2);
    }

    [Fact]
    public void Swap_WhenIndexAreValidAndEqual_ShouldnotSwap()
    {
        //
        var list = new List<int>() { 0, 10, 20, 30, 40, 50, 60 };
        var expectedList = list.ToList(); //Shallow copy

        int index = 2;
        int initialValAtIndex = list.ElementAt(index);
        int otherIndex = index;
        int initialValAtOtherIndex = list.ElementAt(otherIndex);


        //
        list.Swap_(index, otherIndex);
        Assert.Equal(list.ElementAt(index), initialValAtOtherIndex);
        Assert.Equal(list.ElementAt(otherIndex), initialValAtIndex);
        Assert.Equal(list, expectedList);

        list.Swap_(otherIndex, index);
        Assert.Equal(list.ElementAt(index), initialValAtIndex);
        Assert.Equal(list.ElementAt(otherIndex), initialValAtOtherIndex);
        Assert.Equal(list, expectedList);
    }

    [Fact]
    public void Swap_WhenAtLeastOneIndexIsOutOfRange_ShouldThrowAnOutOfRangeIntegerException()
    {
        var list = new List<char>() { 'a', 'b', 'c' };
        var maxIndex = list.Count - 1;
        var invalidIndex = maxIndex + 1;

        var expectedMessage = string.Format(OutOfRangeIntegerException.MESSAGE_FORMAT, "IEnumerable Index", invalidIndex, 0, maxIndex);
        var ex1 = Assert.Throws<OutOfRangeIntegerException>(() => list.Swap_(invalidIndex, 1));
        var ex2 = Assert.Throws<OutOfRangeIntegerException>(() => list.Swap_(1, invalidIndex));
        Assert.Equal(expectedMessage, ex1.Message);
        Assert.Equal(expectedMessage, ex2.Message);
    }
    #endregion Swap_

    #region Shuffle_
    [Fact]
    public void Shuffle_WhenSwappingIndexesAreValid_ShouldShuffleTheElementsAccordingTo()
    {
        //--- Arrange ---
        var list = new List<int>() { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };


        //--- Act ---
        list.Shuffle_(indexesToSwap: new (int index, int otherIndex)[] {
            (10,0), // { 100, 10, 20, 30, 40, 50, 60, 70, 80, 90, 0 }
            (9,1),  // { 100, 90, 20, 30, 40, 50, 60, 70, 80, 10, 0 }
            (8,2),  // { 100, 90, 80, 30, 40, 50, 60, 70, 20, 10, 0 }
            (3,7),  // { 100, 90, 80, 70, 40, 50, 60, 30, 20, 10, 0 }
            (4,6),  // { 100, 90, 80, 70, 60, 50, 40, 30, 20, 10, 0 }
            (5,0),  // { 50, 90, 80, 70, 60, 100, 40, 30, 20, 10, 0 }
            (2,7),  // { 50, 90, 30, 70, 60, 100, 40, 80, 20, 10, 0 }
            (8,10),  // { 50, 90, 30, 70, 60, 100, 40, 80, 0, 10, 20 }
        });

        //--- Assert ---
        List<int> expectedList = new() { 50, 90, 30, 70, 60, 100, 40, 80, 0, 10, 20 };
        Assert.Equal(expectedList, list);
    }

    [Fact]
    public void Shuffle_WhenListIsEmpty_ShouldDoNothing()
    {
        //--- Arrange ---
        var list = new List<int>();
        var expectedList = list.ToList();

        //--- Act ---
        list.Shuffle_(new (int index, int otherIndex)[] {
            (10,0),
            (9,1),
            (8,2),
            (3,7),
            (4,6),
            (5,0),
            (2,7),
            (8,10),
        });

        //--- Assert ---
        Assert.Equal(expectedList, list);
    }

    [Fact]
    public void Shuffle_WhenAtLeastOneIndexIsOutOfRange_ShouldThrowAnOutOfRangeIntegerException()
    {
        //--- Arrange ---
        var list = new List<int>() { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
        var maxIndex = list.Count - 1;
        var invalidIndex = maxIndex + 1;
        var indexesToSwap = new (int index, int otherIndex)[] {
            (10,0),
            (9,1),
            (8,2),
            (3,7),
            (4,6),
            (5,invalidIndex),
            (2,7),
            (8,10),
        };

        //--- Act & Assert ---
        var expectedMessage = string.Format(OutOfRangeIntegerException.MESSAGE_FORMAT, "IEnumerable Index", invalidIndex, 0, maxIndex);
        var ex = Assert.Throws<OutOfRangeIntegerException>(() => list.Shuffle_(indexesToSwap));
        Assert.Equal(expectedMessage, ex.Message);
    }

    #endregion Shuffle_

    #region ReverseShuffle_
    [Fact]
    public void ReverseShuffle_WhenSwappingIndexesAreValid_ShouldShuffleTheElementsAccordingTo()
    {
        //--- Arrange ---
        var list = new List<int>() { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };

        IEnumerable<(int index, int otherIndex)> indexesToSwap = new[] {
            (10,0), // { 80, 90, 100, 20, 60, 0, 40, 30, 70, 10, 50 }  ↑
            (9,1),  // { 50, 90, 100, 20, 60, 0, 40, 30, 70, 10, 80 }  ↑
            (8,2),  // { 50, 10, 100, 20, 60, 0, 40, 30, 70, 90, 80 }  ↑
            (3,7),  // { 50, 10, 70, 20, 60, 0, 40, 30, 100, 90, 80 }  ↑
            (4,6),  // { 50, 10, 70, 30, 60, 0, 40, 20, 100, 90, 80 }  ↑
            (5,0),  // { 50, 10, 70, 30, 40, 0, 60, 20, 100, 90, 80 }  ↑
            (2,7),  // { 0, 10, 70, 30, 40, 50, 60, 20, 100, 90, 80 }  ↑
            (8,10), // { 0, 10, 20, 30, 40, 50, 60, 70, 100, 90, 80 } ↑
        };
        var indexesToSwapCopy = indexesToSwap.ToArray();

        //--- Act ---
        list.ReverseShuffle_(indexesToSwap);

        //--- Assert ---
        List<int> expectedList = new() { 80, 90, 100, 20, 60, 0, 40, 30, 70, 10, 50 };
        Assert.Equal(expectedList, list);

        Assert.Equal(indexesToSwapCopy, indexesToSwap); //The original indexesToSwap must not have been modified.
    }

    [Fact]
    public void ReverseShuffle_WhenApplyingAfterOrBeforeShuffleAsManyTimesAndWithSameSwappingIndexes_ShouldGiveTheOriginalList()
    {
        //--- Arrange ---
        var list = new List<int>() { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
        var originalList = list.ToList();

        var indexesToSwap = new (int index, int otherIndex)[] {
            (10,0),
            (9,1),
            (8,2),
            (3,7),
            (4,6),
            (5,0),
            (2,7),
            (8,10),
        };

        //--- Act & Assert ---
        list.ReverseShuffle_(indexesToSwap);
        list.Shuffle_(indexesToSwap);
        Assert.Equal(originalList, list); //>>>> list est à son état initial

        //--- Act & Assert ---
        list.Shuffle_(indexesToSwap);
        list.ReverseShuffle_(indexesToSwap);
        Assert.Equal(originalList, list); //>>>> list est à son état initial

        //--- Act & Assert ---
        list.Shuffle_(indexesToSwap);
        list.Shuffle_(indexesToSwap);
        list.Shuffle_(indexesToSwap);
        list.ReverseShuffle_(indexesToSwap);
        list.ReverseShuffle_(indexesToSwap);
        list.ReverseShuffle_(indexesToSwap);
        Assert.Equal(originalList, list); //>>>> list est à son état initial

        //--- Act & Assert ---
        list.ReverseShuffle_(indexesToSwap);
        list.ReverseShuffle_(indexesToSwap);
        list.ReverseShuffle_(indexesToSwap);
        list.Shuffle_(indexesToSwap);
        list.Shuffle_(indexesToSwap);
        list.Shuffle_(indexesToSwap);
        Assert.Equal(originalList, list); //>>>> list est à son état initial
    }

    [Fact]
    public void ReverseShuffle_WhenListIsEmpty_ShouldDoNothing()
    {
        //--- Arrange ---
        var list = new List<int>();
        var expectedList = list.ToList();

        //--- Act ---
        list.ReverseShuffle_(new (int index, int otherIndex)[] {
            (10,0),
            (9,1),
            (8,2),
            (3,7),
            (4,6),
            (5,0),
            (2,7),
            (8,10),
        });

        //--- Assert ---
        Assert.Equal(expectedList, list);
    }

    [Fact]
    public void ReverseShuffle_WhenAtLeastOneIndexIsOutOfRange_ShouldThrowAnOutOfRangeIntegerException()
    {
        //--- Arrange ---
        var list = new List<int>() { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
        var maxIndex = list.Count - 1;
        var invalidIndex = maxIndex + 1;
        var indexesToSwap = new (int index, int otherIndex)[] {
            (10,0),
            (9,1),
            (8,2),
            (3,7),
            (4,6),
            (5,invalidIndex),
            (2,7),
            (8,10),
        };

        //--- Act & Assert ---
        var expectedMessage = string.Format(OutOfRangeIntegerException.MESSAGE_FORMAT, "IEnumerable Index", invalidIndex, 0, maxIndex);
        var ex = Assert.Throws<OutOfRangeIntegerException>(() => list.ReverseShuffle_(indexesToSwap));
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion ReverseShuffle_

}