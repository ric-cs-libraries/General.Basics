using Xunit;


using General.Basics.ErrorHandling;
using General.Basics.Generators.Interfaces;

namespace General.Basics.Generators.UnitTests;


// >>>> PARTIALLY TESTED because : Randomness is not testable !
public class IntsTuplesGeneratorTests
{
    private IIntsRandomizer randomizer;

    public IntsTuplesGeneratorTests()
    {
        randomizer = DefaultIntsRandomizer.Create(minValue: -100, maxValue: 250);
    } 

    [Fact]
    public void Create_WhenCalled_ShouldReturnAIntsTuplesGeneratorInstance()
    {
        //--- Act ---
        var instance = IntsTuplesGenerator.Create(randomizer);

        //--- Assert ---
        Assert.IsType<IntsTuplesGenerator>(instance);
    }

    #region GetRandomPairs
    [Theory]
    [InlineData(1)]
    [InlineData(500)]
    public void GetRandomPairs_WhenCalledWithCorrectParamValue_ShouldReturnAnIEnumerableWithThisParamValueNumberOfPairs(int nbPairs)
    {
        //--- Arrange ---
        var intsTuplesGenerator = IntsTuplesGenerator.Create(randomizer);

        //--- Act ---
        IEnumerable<(int, int)> pairs = intsTuplesGenerator.GetRandomPairs(nbPairs);

        //--- Assert ---
        Assert.Equal(nbPairs, pairs.Count());
    }

    [Fact]
    public void GetRandomPairs_WhenCalledWithAParamValueLowerThanOne_ShouldThrowAnIntShouldBeGreaterOrEqualException()
    {
        //--- Arrange ---
        var intsTuplesGenerator = IntsTuplesGenerator.Create(randomizer);
        var invalidNbPairs = 0;
        var minValue = 1;

        //--- Act & Assert ---
        var ex = Assert.Throws<IntShouldBeGreaterOrEqualException>(() => intsTuplesGenerator.GetRandomPairs(invalidNbPairs));

        var expectedMessage = string.Format(IntShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "nbPairs", invalidNbPairs, minValue);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion GetRandomPairs

}
