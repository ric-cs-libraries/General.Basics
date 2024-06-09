using Xunit;


using General.Basics.ErrorHandling;
using General.Basics.Generators.Interfaces;


namespace General.Basics.Generators.UnitTests;


// >>>> PARTIALLY TESTED because : Randomness is not testable !
public class RandomIntsPairsGeneratorTests
{
    private IIntsRandomizer randomizer;

    public RandomIntsPairsGeneratorTests()
    {
        randomizer = DefaultIntsRandomizer.Create(minValue: -100, maxValue: 250);
    }

    [Fact]
    public void Create_WhenCalled_ShouldReturnAIntsTuplesGeneratorInstance()
    {
        //--- Act ---
        var instance = RandomIntsPairsGenerator.Create(randomizer);

        //--- Assert ---
        Assert.IsType<RandomIntsPairsGenerator>(instance);
    }

    #region GetPairs
    [Theory]
    [InlineData(1)]
    [InlineData(500)]
    public void GetPairs_WhenCalledWithCorrectParamValue_ShouldReturnAnIEnumerableWithThisParamValueNumberOfPairs(int nbPairs)
    {
        //--- Arrange ---
        var intsTuplesGenerator = RandomIntsPairsGenerator.Create(randomizer);

        //--- Act ---
        IEnumerable<(int, int)> pairs = intsTuplesGenerator.GetPairs(nbPairs);

        //--- Assert ---
        Assert.Equal(nbPairs, pairs.Count());
    }

    [Fact]
    public void GetPairs_WhenCalledWithAParamValueLowerThanOne_ShouldThrowAnIntShouldBeGreaterOrEqualException()
    {
        //--- Arrange ---
        var intsTuplesGenerator = RandomIntsPairsGenerator.Create(randomizer);
        var invalidNbPairs = 0;
        var minValue = 1;

        //--- Act & Assert ---
        var ex = Assert.Throws<IntShouldBeGreaterOrEqualException>(() => intsTuplesGenerator.GetPairs(invalidNbPairs));

        var expectedMessage = string.Format(IntShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "nbPairs", invalidNbPairs, minValue);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion GetPairs

}
