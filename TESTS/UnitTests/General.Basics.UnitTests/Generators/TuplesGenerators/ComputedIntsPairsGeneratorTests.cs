using General.Basics.Bounds.Intervals;
using General.Basics.ErrorHandling;
using General.Basics.Extensions;
using Xunit;


namespace General.Basics.Generators.TupleGenerators.UnitTests;


public class ComputedIntsPairsGeneratorTests
{
    #region GetPairs

    [Fact]
    public void GetPairs_WhenRightValueMustDifferFromLeftValue_ShouldReturnTheCorrectPairs()
    {
        //--- Arrange ---
        bool distinctValue = true;
        var maxNbPairs_ = 4;
        IntsInterval leftValueAuthorizedInterval = new(minValue: 0, maxValue: 7);
        IntsInterval rightValueAuthorizedInterval = leftValueAuthorizedInterval;

        var pairsLeftValueComputer = (int? previousLeftValue, int iterationNumber, int maxNbPairs, int maxLeftValue) =>
        {
            return maxLeftValue - iterationNumber;
        };

        var pairsRightValueComputer = (int? previousRightValue, int iterationNumber, int maxNbPairs, int maxRightValue, int currentLeftValue) =>
        {
            return iterationNumber + 1;
        };

        var intsPairsGenerator = ComputedIntsPairsGenerator.Create(pairsLeftValueComputer, pairsRightValueComputer);


        //--- Act ---
        IEnumerable<(int, int)> pairs = intsPairsGenerator.GetPairs(maxNbPairs_, leftValueAuthorizedInterval, rightValueAuthorizedInterval, distinctValue);

        //--- Assert ---
        IEnumerable<(int, int)> expectedPairs = new (int, int)[]
        {
           (7,1), (6,2), (5,3), (3,5)    //(4,4) excluded because distinctValue = true
        };
        Assert.Equal(expectedPairs, pairs);
    }

    [Fact]
    public void GetPairs_WhenRightValueCanBeEqualToLeftValue_ShouldReturnTheCorrectPairs()
    {
        //--- Arrange ---
        bool distinctValue = false;
        var maxNbPairs_ = 5;
        IntsInterval leftValueAuthorizedInterval = new(minValue: 0, maxValue: 7);
        IntsInterval rightValueAuthorizedInterval = leftValueAuthorizedInterval;

        var pairsLeftValueComputer = (int? previousLeftValue, int iterationNumber, int maxNbPairs, int maxLeftValue) =>
        {
            return maxLeftValue - iterationNumber;
        };

        var pairsRightValueComputer = (int? previousRightValue, int iterationNumber, int maxNbPairs, int maxRightValue, int currentLeftValue) =>
        {
            return iterationNumber + 1;
        };

        var intsPairsGenerator = ComputedIntsPairsGenerator.Create(pairsLeftValueComputer, pairsRightValueComputer);


        //--- Act ---
        IEnumerable<(int, int)> pairs = intsPairsGenerator.GetPairs(maxNbPairs_, leftValueAuthorizedInterval, rightValueAuthorizedInterval, distinctValue);

        //--- Assert ---
        IEnumerable<(int, int)> expectedPairs = new (int, int)[]
        {
           (7,1), (6,2), (5,3), (4,4), (3,5)
        };
        Assert.Equal(expectedPairs, pairs);
    }

    [Fact]
    public void GetPairs_WhenRightValueMustDifferFromLeftValue_ShouldReturnTheCorrectPairs_2()
    {
        //--- Arrange ---
        bool distinctValue = true;
        var maxNbPairs_ = 10;
        IntsInterval leftValueAuthorizedInterval = new(minValue: 0, maxValue: 7);
        IntsInterval rightValueAuthorizedInterval = new(minValue: 0, maxValue: 14);

        var pairsLeftValueComputer = (int? previousLeftValue, int iterationNumber, int maxNbPairs, int maxLeftValue) =>
        {
            return maxLeftValue - iterationNumber;
        };

        var pairsRightValueComputer = (int? previousRightValue, int iterationNumber, int maxNbPairs, int maxRightValue, int currentLeftValue) =>
        {
            return currentLeftValue + 1 + 3 * iterationNumber;
        };

        var intsPairsGenerator = ComputedIntsPairsGenerator.Create(pairsLeftValueComputer, pairsRightValueComputer);


        //--- Act ---
        IEnumerable<(int, int)> pairs = intsPairsGenerator.GetPairs(maxNbPairs_, leftValueAuthorizedInterval, rightValueAuthorizedInterval, distinctValue);

        //--- Assert ---
        IEnumerable<(int, int)> expectedPairs = new (int, int)[]
        {
           (7,8), (6,10), (5,12), (4,14)
        };
        Assert.Equal(expectedPairs, pairs);
    }


    [Fact]
    public void GetPairs_WhenRightValueMustDifferFromLeftValue_ShouldReturnTheCorrectPairs_3()
    {
        //--- Arrange ---
        bool distinctValue = true;
        var maxNbPairs_ = 15;
        IntsInterval leftValueAuthorizedInterval = new(minValue: 0, maxValue: 7);
        IntsInterval rightValueAuthorizedInterval = new(minValue: -1, maxValue: 8);

        var pairsLeftValueComputer = (int? previousLeftValue, int iterationNumber, int maxNbPairs, int maxLeftValue) =>
        {
            return iterationNumber;
        };

        var pairsRightValueComputer = (int? previousRightValue, int iterationNumber, int maxNbPairs, int maxRightValue, int currentLeftValue) =>
        {
            return iterationNumber + ((previousRightValue.HasValue) ? previousRightValue.Value : 3);
        };

        var intsPairsGenerator = ComputedIntsPairsGenerator.Create(pairsLeftValueComputer, pairsRightValueComputer);


        //--- Act ---
        IEnumerable<(int, int)> pairs = intsPairsGenerator.GetPairs(maxNbPairs_, leftValueAuthorizedInterval, rightValueAuthorizedInterval, distinctValue);

        //--- Assert ---
        IEnumerable<(int, int)> expectedPairs = new (int, int)[]
        {
           (0,3), (1,4), (2,6)  //Stops ther, because rightValue would get higher(9=3+6) than its maxValue (7)
        };
        Assert.Equal(expectedPairs, pairs);
    }


    [Fact]
    public void GetPairs_WhenRightValueMustDifferFromLeftValue_ShouldReturnTheCorrectPairs_4()
    {
        //--- Arrange ---
        bool distinctValue = true;
        var maxNbPairs_ = 15;
        IntsInterval leftValueAuthorizedInterval = new(minValue: 0, maxValue: 7);
        IntsInterval rightValueAuthorizedInterval = leftValueAuthorizedInterval;

        var pairsLeftValueComputer = (int? previousLeftValue, int iterationNumber, int maxNbPairs, int maxLeftValue) =>
        {
            return maxLeftValue - iterationNumber;
        };

        var pairsRightValueComputer = (int? previousRightValue, int iterationNumber, int maxNbPairs, int maxRightValue, int currentLeftValue) =>
        {
            int delta1 = (maxRightValue + 1) / 4;
            int delta2 = delta1 / 2;

            int rightValue;
            if (!previousRightValue.HasValue)
            {
                rightValue = maxRightValue - delta2;
            }
            else
            {
                rightValue = previousRightValue.Value + ((iterationNumber.IsOdd_()) ? -delta1 : +delta2);
            }

            return rightValue;
        };

        var intsPairsGenerator = ComputedIntsPairsGenerator.Create(pairsLeftValueComputer, pairsRightValueComputer);


        //--- Act ---
        IEnumerable<(int, int)> pairs = intsPairsGenerator.GetPairs(maxNbPairs_, leftValueAuthorizedInterval, rightValueAuthorizedInterval, distinctValue);

        //--- Assert ---
        IEnumerable<(int, int)> expectedPairs = new (int, int)[]
        {
           (7,6), (6,4), (4,3), (3,4), (1,3), (0,1)
        };
        Assert.Equal(expectedPairs, pairs);
    }


    [Fact]
    public void GetPairs_WhenRightValueCanBeEqualToLeftValue_ShouldReturnTheCorrectPairs_2()
    {
        //--- Arrange ---
        bool distinctValue = false;
        var maxNbPairs_ = 15;
        IntsInterval leftValueAuthorizedInterval = new(minValue: 0, maxValue: 7);
        IntsInterval rightValueAuthorizedInterval = leftValueAuthorizedInterval;

        var pairsLeftValueComputer = (int? previousLeftValue, int iterationNumber, int maxNbPairs, int maxLeftValue) =>
        {
            return maxLeftValue - iterationNumber;
        };

        var pairsRightValueComputer = (int? previousRightValue, int iterationNumber, int maxNbPairs, int maxRightValue, int currentLeftValue) =>
        {
            int delta1 = (maxRightValue + 1) / 4;
            int delta2 = delta1 / 2;

            int rightValue;
            if (!previousRightValue.HasValue)
            {
                rightValue = maxRightValue - delta2;
            }
            else
            {
                rightValue = previousRightValue.Value + ((iterationNumber.IsOdd_()) ? -delta1 : +delta2);
            }

            return rightValue;
        };

        var intsPairsGenerator = ComputedIntsPairsGenerator.Create(pairsLeftValueComputer, pairsRightValueComputer);


        //--- Act ---
        IEnumerable<(int, int)> pairs = intsPairsGenerator.GetPairs(maxNbPairs_, leftValueAuthorizedInterval, rightValueAuthorizedInterval, distinctValue);

        //--- Assert ---
        IEnumerable<(int, int)> expectedPairs = new (int, int)[]
        {
           (7,6), (6,4), (5,5), (4,3), (3,4), (2,2), (1,3), (0,1)
        };
        Assert.Equal(expectedPairs, pairs);
    }


    [Fact]
    public void GetPairs_WhenMaxNbPairsIs1_ShouldReturnTheCorrectOnePairResult()
    {
        //--- Arrange ---
        var maxNbPairs_ = 1;
        IntsInterval leftValueAuthorizedInterval = new(minValue: 0, maxValue: 7);
        IntsInterval rightValueAuthorizedInterval = leftValueAuthorizedInterval;

        var pairsLeftValueComputer = (int? previousLeftValue, int iterationNumber, int maxNbPairs, int maxLeftValue) =>
        {
            return maxLeftValue - iterationNumber;
        };

        var pairsRightValueComputer = (int? previousRightValue, int iterationNumber, int maxNbPairs, int maxRightValue, int currentLeftValue) =>
        {
            int delta1 = (maxRightValue + 1) / 4;
            int delta2 = delta1 / 2;

            int rightValue;
            if (!previousRightValue.HasValue)
            {
                rightValue = maxRightValue - delta2;
            }
            else
            {
                rightValue = previousRightValue.Value + ((iterationNumber.IsOdd_()) ? -delta1 : +delta2);
            }

            return rightValue;
        };

        var intsPairsGenerator = ComputedIntsPairsGenerator.Create(pairsLeftValueComputer, pairsRightValueComputer);


        //--- Act ---
        IEnumerable<(int, int)> pairs = intsPairsGenerator.GetPairs(maxNbPairs_, leftValueAuthorizedInterval, rightValueAuthorizedInterval);

        //--- Assert ---
        IEnumerable<(int, int)> expectedPairs = new (int, int)[]
        {
           (7,6)
        };
        Assert.Equal(expectedPairs, pairs);
    }


    [Fact]
    public void GetPairs_WhenMaxNbPairsIsLowerThan1_ShouldThrowAnIntShouldBeGreaterOrEqualException()
    {
        //--- Arrange ---
        IntsInterval leftValueAuthorizedInterval = new(minValue: 0, maxValue: 7);
        IntsInterval rightValueAuthorizedInterval = leftValueAuthorizedInterval;

        var pairsLeftValueComputer = (int? previousLeftValue, int iterationNumber, int maxNbPairs, int maxLeftValue) =>
        {
            return maxLeftValue - iterationNumber;
        };

        var pairsRightValueComputer = (int? previousRightValue, int iterationNumber, int maxNbPairs, int maxRightValue, int currentLeftValue) =>
        {
            return iterationNumber + 1;
        };

        var intsPairsGenerator = ComputedIntsPairsGenerator.Create(pairsLeftValueComputer, pairsRightValueComputer);
        var invalidNbPairs = 0;
        var minValue = 1;

        //--- Act & Assert ---
        var ex = Assert.Throws<IntShouldBeGreaterOrEqualException>(() => intsPairsGenerator.GetPairs(invalidNbPairs, leftValueAuthorizedInterval, rightValueAuthorizedInterval));

        var expectedMessage = string.Format(IntShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "maxNbPairs", invalidNbPairs, minValue);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void GetPairs_WhenMaxValueIsNullForLeftValueAuthorizedInterval_ShouldThrowAMustNotBeNullException()
    {
        //--- Arrange ---
        var maxNbPairs_ = 4;
        IntsInterval leftValueAuthorizedInterval = new(minValue: 0, maxValue: null);
        IntsInterval rightValueAuthorizedInterval = new(minValue: 0, maxValue: 10);

        var pairsLeftValueComputer = (int? previousLeftValue, int iterationNumber, int maxNbPairs, int maxLeftValue) =>
        {
            return maxLeftValue - iterationNumber;
        };

        var pairsRightValueComputer = (int? previousRightValue, int iterationNumber, int maxNbPairs, int maxRightValue, int currentLeftValue) =>
        {
            return iterationNumber + 1;
        };

        var intsPairsGenerator = ComputedIntsPairsGenerator.Create(pairsLeftValueComputer, pairsRightValueComputer);


        //--- Act && Assert ---

        var ex = Assert.Throws<MustNotBeNullException>(() => intsPairsGenerator.GetPairs(maxNbPairs_, leftValueAuthorizedInterval, rightValueAuthorizedInterval));

        var expectedMessage = string.Format(MustNotBeNullException.MESSAGE_FORMAT, "leftValueAuthorizedInterval.MaxValue");
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void GetPairs_WhenMaxValueIsNullForRightValueAuthorizedInterval_ShouldThrowAMustNotBeNullException()
    {
        //--- Arrange ---
        var maxNbPairs_ = 4;
        IntsInterval leftValueAuthorizedInterval = new(minValue: 0, maxValue: 10);
        IntsInterval rightValueAuthorizedInterval = new(minValue: 0, maxValue: null);

        var pairsLeftValueComputer = (int? previousLeftValue, int iterationNumber, int maxNbPairs, int maxLeftValue) =>
        {
            return maxLeftValue - iterationNumber;
        };

        var pairsRightValueComputer = (int? previousRightValue, int iterationNumber, int maxNbPairs, int maxRightValue, int currentLeftValue) =>
        {
            return iterationNumber + 1;
        };

        var intsPairsGenerator = ComputedIntsPairsGenerator.Create(pairsLeftValueComputer, pairsRightValueComputer);


        //--- Act && Assert ---

        var ex = Assert.Throws<MustNotBeNullException>(() => intsPairsGenerator.GetPairs(maxNbPairs_, leftValueAuthorizedInterval, rightValueAuthorizedInterval));

        var expectedMessage = string.Format(MustNotBeNullException.MESSAGE_FORMAT, "rightValueAuthorizedInterval.MaxValue");
        Assert.Equal(expectedMessage, ex.Message);
    }

    #endregion GetPairs

}
