using General.Basics.ErrorHandling;
using General.Basics.Extensions;
using Xunit;


namespace General.Basics.UnitTests.Extensions.Numbers;

public class IntExtensionTests
{
    #region CheckIsGreaterOrEqualTo_
    [Fact]
    public void CheckIsGreaterOrEqualTo_WhenIntegerHasNotTheMinimalValueAndSubjectNameNotGiven_ShouldThrowAnIntShouldBeGreaterOrEqualExceptionWithTheCorrectMessage()
    {
        var myInt = 9;
        var minimalValue = 10;

        var ex = Assert.Throws<IntShouldBeGreaterOrEqualException>(() => myInt.CheckIsGreaterOrEqualTo_(minimalValue));

        string subjectName = "number";
        var expectedMessage = string.Format(IntShouldBeGreaterOrEqualException.MESSAGE_FORMAT, subjectName, myInt, minimalValue);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void CheckIsGreaterOrEqualTo_WhenIntegerHasNotTheMinimalValueAndSubjectNameGiven_ShouldThrowAnIntShouldBeGreaterOrEqualExceptionWithTheCorrectMessage()
    {
        var myInt = 9;
        var minimalValue = 10;
        string subjectName = nameof(myInt);

        var ex = Assert.Throws<IntShouldBeGreaterOrEqualException>(() => myInt.CheckIsGreaterOrEqualTo_(minimalValue, subjectName));

        var expectedMessage = string.Format(IntShouldBeGreaterOrEqualException.MESSAGE_FORMAT, subjectName, myInt, minimalValue);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void CheckIsGreaterOrEqualTo_WhenIntegerHasTheMinimalValueOrGreater_ShouldNotThrowAnException()
    {
        var minimalValue = 10;
        var myInt1 = minimalValue;
        var myInt2 = minimalValue + 1;

        myInt1.CheckIsGreaterOrEqualTo_(minimalValue);
        myInt2.CheckIsGreaterOrEqualTo_(minimalValue);
        myInt1.CheckIsGreaterOrEqualTo_(minimalValue, "xxx");
        myInt2.CheckIsGreaterOrEqualTo_(minimalValue, "zzz");

        Assert.True(true);
    }
    #endregion CheckIsGreaterOrEqualTo_

    #region IsDivisibleBy_

    [Theory]
    [ClassData(typeof(IntTestsData.DivisibleByData))]
    public void IsDivisibleBy_WhenIsDivisible_ShouldReturnTrue(int dividende, int divider)
    {
        //--- Act ---
        var result = dividende.IsDivisibleBy_(divider);

        //--- Assert ---
        Assert.True(result);
    }

    [Theory]
    [ClassData(typeof(IntTestsData.NotDivisibleByData))]
    public void IsDivisibleBy_WhenIsNotDivisible_ShouldReturnFalse(int dividende, int divider)
    {
        //--- Act ---
        var result = dividende.IsDivisibleBy_(divider);

        //--- Assert ---
        Assert.False(result);
    }
    #endregion IsDivisibleBy_

    #region IsPair_
    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(6)]
    [InlineData(72)]
    [InlineData(-2)]
    [InlineData(-68)]
    public void IsPair_WhenIsPair_ShouldReturnTrue(int number)
    {
        //--- Act ---
        var result = number.IsPair_();

        //--- Assert ---
        Assert.True(result);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(75)]
    [InlineData(-1)]
    [InlineData(-7)]
    public void IsPair_WhenIsNotPair_ShouldReturnFalse(int number)
    {
        //--- Act ---
        var result = number.IsPair_();

        //--- Assert ---
        Assert.False(result);
    }
    #endregion IsPair_


    #region IsOdd_
    [Theory]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(75)]
    [InlineData(-1)]
    [InlineData(-7)]
    public void IsOdd_WhenIsOdd_ShouldReturnTrue(int number)
    {
        //--- Act ---
        var result = number.IsOdd_();

        //--- Assert ---
        Assert.True(result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(6)]
    [InlineData(72)]
    [InlineData(-2)]
    [InlineData(-68)]
    public void IsOdd_WhenIsNotOdd_ShouldReturnFalse(int number)
    {
        //--- Act ---
        var result = number.IsOdd_();

        //--- Assert ---
        Assert.False(result);
    }
    #endregion IsOdd_

    #region IsBetween_
    [Theory]
    [InlineData(-4)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(6)]
    [InlineData(8)]
    [InlineData(9)]
    public void IsBetween_WhenIsBetweenBoundsIncluded_ShouldReturnTrue(int number)
    {
        //--- Act ---
        var result = number.IsBetween_(-4, 9);

        //--- Assert ---
        Assert.True(result);
    }

    [Fact]
    public void IsBetween_WhenBoundsAreEqualAndNumberEqualsToBounds_ShouldReturnTrue()
    {
        //
        int number = -4;
        int boundsValue = number;

        //--- Act ---
        var result = number.IsBetween_(boundsValue, boundsValue);

        //--- Assert ---
        Assert.True(result);
    }

    [Theory]
    [InlineData(-15)]
    [InlineData(-5)]
    [InlineData(11)]
    [InlineData(23)]
    public void IsBetween_WhenIsNotBetweenBoundsIncluded_ShouldReturnFalse(int number)
    {
        //--- Act ---
        var result = number.IsBetween_(-4, 9);

        //--- Assert ---
        Assert.False(result);
    }

    [Fact]
    public void IsBetween_WhenBoundsAreEqualAndNumberIsNotEqualToBounds_ShouldReturnFalse()
    {
        //
        int number = 5;
        int boundsValue = number + 1;

        //--- Act ---
        var result = number.IsBetween_(boundsValue, boundsValue);

        //--- Assert ---
        Assert.False(result);
    }
    #endregion IsBetween_


    #region IsInRange_
    [Theory]
    [InlineData(0)]
    [InlineData(6)]
    [InlineData(8)]
    [InlineData(9)]
    public void IsInRange_WhenIsInRangeEndBoundExcluded_ShouldReturnTrue(int number)
    {
        //--- Act ---
        var result = number.IsInRange_(0..10); //No negative accepted

        //--- Assert ---
        Assert.True(result);
    }
    [Theory]
    [InlineData(0)]
    [InlineData(9)]
    public void IsInRange_WhenIsInRangeEndBoundExcluded_ShouldReturnFalse(int number)
    {
        //--- Act ---
        var result = number.IsInRange_(1..9); //No negative accepted

        //--- Assert ---
        Assert.False(result);
    }
    #endregion IsInRange_




    //==============================================================================================
    static class IntTestsData
    {
        internal class DivisibleByData : TheoryData<int, int>
        {
            public DivisibleByData()
            {
                Add(10, 5);
                Add(10, 2);
                Add(10, 1);
                Add(10, 10);

                Add(33, 3);
                Add(33, 11);
                Add(33, 33);

                Add(27, 9);
                Add(27, 3);
                Add(27, 27);

                Add(49, 7);
                Add(49, 49);

                Add(0, 1);
                Add(0, 2);
                Add(0, 3);
                Add(0, 4);

                Add(-10, 5);
                Add(-10, 2);
                Add(-10, 1);
                Add(-10, 10);

                Add(-10, -5);
            }
        }

        internal class NotDivisibleByData : TheoryData<int, int>
        {
            public NotDivisibleByData()
            {
                Add(10, 4);
                Add(10, 3);
                Add(10, 0);
                Add(-10, 4);
                Add(-10, -4);

                Add(33, 2);
                Add(33, 10);

                Add(27, 8);
                Add(27, 4);
                Add(27, 0);

                Add(0, 0);
            }
        }

    }
}
