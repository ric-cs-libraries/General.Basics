using Xunit;



using General.Basics.Generators.Shufflers.Interfaces;
using General.Basics.Generators.Shufflers;
using General.Basics.ErrorHandling;

namespace General.Basics.Generators.Shufflers.UnitTests;

public class DefaultShufflerTests
{

    #region Shuffle
    [Fact]
    public void Shuffle__ShouldShuffleAccordingToTheComputedSwappedIndexes()
    {
        //--- Arrange ---
        IShuffler shuffler = new DefaultShuffler();
        List<char> list = new() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };


        //--- Act ---
        shuffler.Shuffle(list);

        //--- Assert ---
        List<char> expectedShuffledList = new() { 'D', 'A', 'C', 'B', 'H', 'F', 'E', 'G' };
        Assert.Equal(expectedShuffledList, list);

        List<(int Index, int OtherIndex)> expectedComputedSwappedIndexes = new()
        {
            (7,6), (6,4), (4,3), (3,4), (1,3), (0,1)
        };
        Assert.Equal(expectedComputedSwappedIndexes, shuffler.LastSwappedIndexes);

    }


    [Fact]
    public void Shuffle__ShouldShuffleAccordingToTheComputedSwappedIndexes_2()
    {
        //--- Arrange ---
        IShuffler shuffler = new DefaultShuffler();
        List<char> list = new() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };


        //--- Act ---
        shuffler.Shuffle(list);

        //--- Assert ---
        List<char> expectedShuffledList = new() { 'C', 'F', 'X', 'O', 'G', 'P', 'I', 'J', 'R', 'L', 'M', 'D', 'A', 'V', 'B', 'S', 'E', 'U', 'H', 'Z', 'K', 'Y', 'N', 'T', 'Q', 'W' };
        Assert.Equal(expectedShuffledList, list);
    }

    [Fact]
    public void Shuffle__ShouldShuffleAccordingToTheComputedSwappedIndexes_3()
    {
        //--- Arrange ---
        IShuffler shuffler = new DefaultShuffler();
        List<char> list = new() { 'A', 'B', 'C', 'D' };


        //--- Act ---
        shuffler.Shuffle(list);

        //--- Assert ---
        List<char> expectedShuffledList = new() { 'C', 'A', 'B', 'D' };
        Assert.Equal(expectedShuffledList, list);
    }

    [Fact]
    public void Shuffle__ShouldShuffleAccordingToTheComputedSwappedIndexes_4()
    {
        //--- Arrange ---
        IShuffler shuffler = new DefaultShuffler();
        List<char> list = new() { 'A', 'B', 'C' };


        //--- Act ---
        shuffler.Shuffle(list);

        //--- Assert ---
        List<char> expectedShuffledList = new() { 'B', 'C', 'A' };
        Assert.Equal(expectedShuffledList, list);
    }


    [Fact]
    public void Shuffle_WhenListOnlyContains2Items_ShouldJustSwapThem()
    {
        //--- Arrange ---
        IShuffler shuffler = new DefaultShuffler();
        List<char> list = new() { 'A', 'B' };


        //--- Act ---
        shuffler.Shuffle(list);

        //--- Assert ---
        List<char> expectedShuffledList = new() { 'B', 'A' };
        Assert.Equal(expectedShuffledList, list);

        List<(int Index, int OtherIndex)> expectedComputedSwappedIndexes = new()
        {
            (0,1)
        };
        Assert.Equal(expectedComputedSwappedIndexes, shuffler.LastSwappedIndexes);
    }

    [Fact]
    public void Shuffle_WhenListOnlyContains1Item_ShouldnotChangeTheListAndLastSwappedIndexesShouldnotChange()
    {
        //--- Arrange ---
        IShuffler shuffler = new DefaultShuffler();
        var initialLastSwappedIndexes = shuffler.LastSwappedIndexes?.ToList();
        List<char> list = new() { 'A' };


        //--- Act ---
        shuffler.Shuffle(list);

        //--- Assert ---
        List<char> expectedShuffledList = new() { 'A' };
        Assert.Equal(expectedShuffledList, list);

        Assert.Equal(initialLastSwappedIndexes, shuffler.LastSwappedIndexes); //null
    }


    [Fact]
    public void Shuffle_WhenListIsEmpty_ShouldnotChangeTheListAndLastSwappedIndexesShouldnotChange()
    {
        //--- Arrange ---
        IShuffler shuffler = new DefaultShuffler();
        var initialLastSwappedIndexes = shuffler.LastSwappedIndexes?.ToList();
        List<char> list = new();


        //--- Act ---
        shuffler.Shuffle(list);

        //--- Assert ---
        Assert.Empty(list);

        Assert.Equal(initialLastSwappedIndexes, shuffler.LastSwappedIndexes); //null
    }

    #endregion Shuffle

    #region GetIndexesToSwap
    [Fact]
    public void GetIndexesToSwap_WhenListLengthIsGreaterThan1_ShouldReturnTheCorrectComputedIndexesToSwap()
    {
        //--- Arrange ---
        IShuffler shuffler = new DefaultShuffler();
        int listLength = 8;


        //--- Act ---
        IEnumerable<(int Index, int OtherIndex)> indexesToSwap = shuffler.GetIndexesToSwap(listLength);

        //--- Assert ---
        List<(int Index, int OtherIndex)> expectedComputedSwappedIndexes = new()
        {
            (7,6), (6,4), (4,3), (3,4), (1,3), (0,1)
        };
        Assert.Equal(expectedComputedSwappedIndexes, indexesToSwap);
    }

    [Fact]
    public void GetIndexesToSwap_WhenListLengthIsGreaterThan1_ShouldReturnTheCorrectComputedIndexesToSwap_2()
    {
        //--- Arrange ---
        IShuffler shuffler = new DefaultShuffler();
        int listLength = 2;


        //--- Act ---
        IEnumerable<(int Index, int OtherIndex)> indexesToSwap = shuffler.GetIndexesToSwap(listLength);

        //--- Assert ---
        List<(int Index, int OtherIndex)> expectedComputedSwappedIndexes = new()
        {
            (0,1)
        };
        Assert.Equal(expectedComputedSwappedIndexes, indexesToSwap);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void GetIndexesToSwap_WhenListLengthIsPositiveButLowerThan2_ShouldReturnAnEmptyIEnumerable(int listLength)
    {
        //--- Arrange ---
        IShuffler shuffler = new DefaultShuffler();


        //--- Act ---
        IEnumerable<(int Index, int OtherIndex)> indexesToSwap = shuffler.GetIndexesToSwap(listLength);

        //--- Assert ---
        Assert.Empty(indexesToSwap);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-2)]
    public void GetIndexesToSwap_WhenListLengthIsNegative_ShouldReturnAMustBePositiveIntegerException(int listLength)
    {
        //--- Arrange ---
        IShuffler shuffler = new DefaultShuffler();


        //--- Act & Assert ---
        var ex = Assert.Throws<MustBePositiveIntegerException>(() => shuffler.GetIndexesToSwap(listLength));

        var expectedMessage = string.Format(MustBePositiveIntegerException.MESSAGE_FORMAT, "List length", listLength);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion GetIndexesToSwap

}
