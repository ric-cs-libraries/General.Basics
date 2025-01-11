using Xunit;


using General.Basics.ErrorHandling;
using General.Basics.Generators.Extensions;


namespace General.Basics.Generators.Extensions.UnitTests;

// >>>> PARTIALLY TESTED because : Randomness is not testable !
public class IListExtensionTests
{
    #region RandomShuffle_

    //[Fact] //Randomness Non testable, mais sert pour debugage éventuel.
    //public void RandomShuffle_WhenCalledWithCorrectParamValue_ShouldShuffleRandomly()
    //{
    //    //--- Arrange ---
    //    var list = new List<int> { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
    //    int nbSwaps = 1000;

    //    //--- Act ---
    //    list.RandomShuffle_(); //Will use default nbSwaps
    //    list.RandomShuffle_(nbSwaps);

    //    //
    //    Assert.True(true);
    //}

    [Fact]
    public void RandomShuffle_WhenListIsEmpty_ShouldDoNothing()
    {
        //--- Arrange ---
        var list = new List<int>();
        var expectedList = list.ToList();

        //--- Act ---
        list.RandomShuffle_(nbSwaps: 45);

        //--- Assert ---
        Assert.Empty(list);
        Assert.Equal(expectedList, list);
    }


    [Fact]
    public void RandomShuffle_WhenCalledWithNegativeParamValue_ShouldThrowAnIntShouldBeGreaterOrEqualException()
    {
        //--- Arrange ---
        var list = new List<int> { 10, 11, 12 };
        int invalidNbSwaps = -1;

        //--- Act & Assert ---
        var ex = Assert.Throws<IntShouldBeGreaterOrEqualException>(() => list.RandomShuffle_(invalidNbSwaps));

        var expectedMessage = string.Format(IntShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "nbSwaps", invalidNbSwaps, 0);
        Assert.Equal(expectedMessage, ex.Message);
    }

    #endregion RandomShuffle_
}
