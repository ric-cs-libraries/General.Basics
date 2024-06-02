using Xunit;


using General.Basics.ErrorHandling;


namespace General.Basics.Generators.UnitTests;


// >>>> PARTIALLY TESTED because : Randomness is not REALLY testable !
public class DefaultIntsRandomizerTests
{
    [Fact]
    public void Create_WhenCalledWithCorrectParams_ShouldReturnADefaultIntsRandomizerInstance()
    {
        //--- Arrange ---
        int minValue = 0;
        int maxValue = 50;

        //--- Act ---
        var instance = DefaultIntsRandomizer.Create(minValue, maxValue);

        //--- Assert ---
        Assert.IsType<DefaultIntsRandomizer>(instance);
    }

    [Fact]
    public void Create_WhenCalledWithInconsistentParams_ShouldThrowAnIntShouldBeGreaterOrEqualException()
    {
        //--- Arrange ---
        int minVal = 50;
        int maxVal = 0;

        //--- Act & Assert ---
        var ex = Assert.Throws<IntShouldBeGreaterOrEqualException>(() => DefaultIntsRandomizer.Create(minVal, maxVal));

        var expectedMessage = string.Format(IntShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "maxValue", maxVal, minVal);
        Assert.Equal(expectedMessage, ex.Message);
    }

    #region GetRandomValue
    //[Fact]
    ////Ce test peut parfois ne pas passer (� peu de choses pr�s) + un peu Gourmand en CPU et timing.
    //public void GetRandomValue_WhenCalledManyTimes_ShouldReturnARatherEquitableProportionOfEachPossibleResult()
    //{
    //    //--- Arrange ---
    //    int minVal = 0;
    //    int maxVal = 19;
    //    int nbPossibleResults = maxVal + 1;
    //    var randomizer = DefaultIntsRandomizer.Create(minVal, maxVal);
    //    var nbRandoms = 3000; //<<<<<<<<<<<< (nb. de titrages), plus il est �lev�, plus bien s�r l'�quilibre est probable, et l'Assert final a des chances de passer.
    //    double equitablePercentage = 100d / nbPossibleResults; //Pourcentage d'occurence moyen attendu pour chaque r�sultat possible.
    //    double percentageTolerance = 1.0d; //+ ou -  1%  autour de la valeur �quitable (equitablePercentage)

    //    //--- Act ---
    //    IEnumerable<int> results = Enumerable.Range(1, nbRandoms).Select(n => randomizer.GetRandomValue());  //Liste des num�ros sortis

    //    //--- Asserts ---
    //    IEnumerable<(int, double pourcentage)> percentageOfEachResult =
    //        Enumerable.Range(minVal, nbPossibleResults)
    //        .Select(possibleResult => ( //Tuple
    //                    res: possibleResult,
    //                    percentage: 100d * Convert.ToDouble(results.Count(result => result == possibleResult)) / Convert.ToDouble(nbRandoms)
    //                ))
    //        .OrderBy(stat => stat.percentage);

    //    IEnumerable<(int, double)> nonEquitablePercentageForSomeResults = //Recherche des pourcentages trop �lev�s ou trop grands d'occurence.
    //        percentageOfEachResult.Where(stat => (stat.pourcentage < equitablePercentage - percentageTolerance) || (stat.pourcentage > equitablePercentage + percentageTolerance));

    //    Assert.Empty(nonEquitablePercentageForSomeResults); //Id�alement vide.
    //}
    #endregion GetRandomValue

}
