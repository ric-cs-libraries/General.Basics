using General.Basics.Bounds.Exceptions;
using General.Basics.Bounds.Intervals;
using System.ComponentModel.DataAnnotations;
using Xunit;


namespace General.Basics.UnitTests.Bounds.Intervals;

public class IntsIntervalTests
{

    [Theory]
    [InlineData(-10, 31)]
    [InlineData(-1, -1)]
    [InlineData(0, 0)]
    [InlineData(100, 100)]
    [InlineData(100, 101)]
    public void Instanciation_WhenMinValueIsLowerOrEqualToMaxValue_ShouldInitializeCorrectlyTheProperties(int minValue, int maxValue)
    {
        //--- Act ---
        IntsInterval interval = new(minValue, maxValue);

        //--- Assert ---
        Assert.Equal(interval.MinValue, minValue);
        Assert.Equal(interval.MaxValue, maxValue);
    }

    [Theory]
    [InlineData(10, 9)]
    [InlineData(0, -1)]
    [InlineData(-1, -2)]
    public void Instanciation_WhenMinValueIsGreaterThanMaxValue_ShouldThrowAValueShouldNotBeGreaterThanException(int minValue, int maxValue)
    {

        //--- Act & Assert ---
        var ex = Assert.Throws<ValueShouldBeLowerOrEqualToException<int?>>(() => new IntsInterval(minValue, maxValue));

        var expectedMessage = string.Format(ValueShouldBeLowerOrEqualToException<int>.MESSAGE_FORMAT, "Interval minValue", minValue, maxValue);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void Instanciation_WhenMinValueIsNull_IsMinShouldBeFalse()
    {
        //
        IntsInterval interval = new(minValue: null, maxValue: 100);

        //
        Assert.False(interval.Bounds.IsMinDefined);
    }

    [Fact]
    public void Instanciation_WhenMaxValueIsNull_IsMaxShouldBeFalse()
    {
        //
        IntsInterval interval = new(minValue: -2, maxValue: null);

        //
        Assert.False(interval.Bounds.IsMaxDefined);
    }

    #region Contains(T)
    [Theory]
    [InlineData(-11, -10, -9)]
    [InlineData(-11, -11, -9)]
    [InlineData(-11, -11, -11)]
    [InlineData(-11, -9, -9)]
    [InlineData(-1, 0, 1)]
    [InlineData(-2, 0, 2)]
    [InlineData(8, 11, 15)]
    [InlineData(11, 11, 11)]
    [InlineData(8, 8, 15)]
    [InlineData(8, 15, 15)]
    public void Contains_WhenValueIsBetweenMinValueAndMaxValueIncluded_ShouldReturnTrue(int minValue, int value, int maxValue)
    {
        //--- Arrange ---
        IntsInterval interval = new(minValue, maxValue);


        //--- Act ---
        bool contains = interval.Contains(value);

        //--- Assert ---
        Assert.True(contains);
    }

    [Theory]
    [InlineData(-11, -12, -9)]
    [InlineData(-11, -8, -9)]
    [InlineData(-1, -2, 1)]
    [InlineData(-1, 2, 1)]
    [InlineData(0, 2, 1)]
    [InlineData(0, 3, 2)]
    [InlineData(-1, 3, 2)]
    [InlineData(8, 7, 15)]
    [InlineData(8, 16, 15)]
    public void Contains_WhenValueIsNotBetweenMinValueAndMaxValueIncluded_ShouldReturnFalse(int minValue, int value, int maxValue)
    {
        //--- Arrange ---
        IntsInterval interval = new(minValue, maxValue);


        //--- Act ---
        bool contains = interval.Contains(value);

        //--- Assert ---
        Assert.False(contains);
    }

    [Theory]
    [InlineData(10, 10, true)]
    [InlineData(10, 11, true)]
    [InlineData(10, 10000, true)]
    [InlineData(10, 9, false)]
    [InlineData(10, 8, false)]
    public void Contains_WhenMaxValueIsNull_ShouldReturnTrueOnlyIfValueIsGreaterOrEqualMinValue(int minValue, int value, bool expectedResult)
    {
        //--- Arrange ---
        IntsInterval interval = new(minValue, maxValue: null);


        //--- Act ---
        bool contains = interval.Contains(value);

        //--- Assert ---
        Assert.Equal(expectedResult, contains);
    }

    [Theory]
    [InlineData(10, 10, true)]
    [InlineData(10, 8, true)]
    [InlineData(10, -1000, true)]
    [InlineData(10, 11, false)]
    [InlineData(10, 12, false)]
    [InlineData(10, 1200, false)]
    public void Contains_WhenMinValueIsNull_ShouldReturnTrueOnlyIfValueIsLowerOrEqualMaxValue(int maxValue, int value, bool expectedResult)
    {
        //--- Arrange ---
        IntsInterval interval = new(minValue: null, maxValue);


        //--- Act ---
        bool contains = interval.Contains(value);

        //--- Assert ---
        Assert.Equal(expectedResult, contains);
    }

    [Theory]
    [InlineData(-3000_000)]
    [InlineData(-12)]
    [InlineData(10)]
    [InlineData(123)]
    [InlineData(1000)]
    [InlineData(1000_000)]
    public void Contains_WhenMinValueIsNullAndMaxValueIsNull_ShouldAlwaysReturnTrue(int value)
    {
        //--- Arrange ---
        IntsInterval interval = new(minValue: null, maxValue: null);


        //--- Act ---
        bool contains = interval.Contains(value);

        //--- Assert ---
        Assert.True(contains);
    }
    #endregion Contains(T)

    #region Intersects
    [Theory]
    [ClassData(typeof(Fixtures.Intersects__ShouldReturnTheCorrectBool_Data))]
    public void Intersects__ShouldReturnTheCorrectBool(IntsInterval intsInterval1, IntsInterval intsInterval2, bool expectedResult)
    {
        //--- Act ---
        bool result = intsInterval1.Intersects(intsInterval2);

        //--- Assert ---
        Assert.Equal(expectedResult, result);
    }
    #endregion Intersects

    #region Contains(IntsInterval)
    [Theory]
    [ClassData(typeof(Fixtures.Contains__ShouldReturnTheCorrectBool_Data))]
    public void Contains__ShouldReturnTheCorrectBool(IntsInterval intsInterval1, IntsInterval intsInterval2, bool expectedResult)
    {
        //--- Act ---
        bool result = intsInterval1.Contains(intsInterval2);

        //--- Assert ---
        Assert.Equal(expectedResult, result);
    }
    #endregion Contains(IntsInterval)

    //==============================================================================================
    //==============================================================================================

    static class Fixtures
    {
        internal class Intersects__ShouldReturnTheCorrectBool_Data : TheoryData<IntsInterval, IntsInterval, bool>
        {
            public Intersects__ShouldReturnTheCorrectBool_Data()
            {
                int int01 = 30;
                int int02 = 50;
                
                IntsInterval intsIntervalRef0 = new(int01, int02);

                Add(intsIntervalRef0, new IntsInterval(int01 - 10, int01 - 1), false);
                Add(intsIntervalRef0, new IntsInterval(int02 + 1, int02 + 10), false);

                Add(intsIntervalRef0, new IntsInterval(int01 + 1, int02 - 1), true);
                Add(intsIntervalRef0, new IntsInterval(int01, int02), true);

                Add(intsIntervalRef0, new IntsInterval(int01 - 1, int01), true);
                Add(intsIntervalRef0, new IntsInterval(int02, int02 + 1), true);

                Add(intsIntervalRef0, new IntsInterval(int01 - 1, int01 + 2), true);
                Add(intsIntervalRef0, new IntsInterval(int02 - 1, int02 + 2), true);

                Add(intsIntervalRef0, new IntsInterval(int01 - 1, int02 + 2), true);

                Add(intsIntervalRef0, new IntsInterval(int01, int01), true);
                Add(intsIntervalRef0, new IntsInterval(int02, int02), true);

                int int03 = 90;
                Add(new IntsInterval(null, null), intsIntervalRef0, true);
                Add(new IntsInterval(null, null), new IntsInterval(int03, null), true);
                Add(new IntsInterval(null, null), new IntsInterval(null, int03), true);
                Add(new IntsInterval(null, null), new IntsInterval(null, null), true);

                Add(new IntsInterval(int01, null), intsIntervalRef0, true);
                Add(new IntsInterval(int01, null), new IntsInterval(int03, null), true);
                Add(new IntsInterval(int01, null), new IntsInterval(int03, int03 + 1), true);
                Add(new IntsInterval(int01, null), new IntsInterval(int01, int01), true);
                Add(new IntsInterval(int01, null), new IntsInterval(int01 - 1, int01), true);
                Add(new IntsInterval(int01, null), new IntsInterval(int01 - 2, int01 - 1), false);
                Add(new IntsInterval(int01, null), new IntsInterval(null, int01 - 1), false);
                Add(new IntsInterval(int01, null), new IntsInterval(int01 - 10, null), true);

                Add(new IntsInterval(null, int01), new IntsInterval(int01 - 1, int01), true);
                Add(new IntsInterval(null, int01), new IntsInterval(int01 - 1, int01 + 1), true);
                Add(new IntsInterval(null, int01), new IntsInterval(int01 - 2, int01 - 1), true);
                Add(new IntsInterval(null, int01), new IntsInterval(int01, int01 + 1), true);
                Add(new IntsInterval(null, int01), new IntsInterval(int01, int01), true);
                Add(new IntsInterval(null, int01), new IntsInterval(int01 + 1, int01 + 2), false);
                Add(new IntsInterval(null, int01), new IntsInterval(int01 + 1, null), false);
            }

            private new void Add(IntsInterval intsInterval1, IntsInterval intsInterval2, bool expectedResult)
            {
                base.Add(intsInterval1, intsInterval2, expectedResult);

                //La réciproque doit donner le même résultat
                base.Add(intsInterval2, intsInterval1, expectedResult);
            }
        }

        internal class Contains__ShouldReturnTheCorrectBool_Data : TheoryData<IntsInterval, IntsInterval, bool>
        {
            public Contains__ShouldReturnTheCorrectBool_Data()
            {
                int int01 = 30;
                int int02 = 50;

                IntsInterval intsIntervalRef0 = new(int01, int02);

                Add(intsIntervalRef0, new IntsInterval(int01 - 10, int01 - 1), false);
                Add(intsIntervalRef0, new IntsInterval(int02 + 1, int02 + 10), false);

                Add(intsIntervalRef0, new IntsInterval(int01 + 1, int02 - 1), true);
                Add(new IntsInterval(int01 + 1, int02 - 1), intsIntervalRef0, false);
                Add(intsIntervalRef0, new IntsInterval(int01, int02), true);
                Add(new IntsInterval(int01, int02), intsIntervalRef0, true);

                Add(intsIntervalRef0, new IntsInterval(int01 - 1, int01), false);
                Add(intsIntervalRef0, new IntsInterval(int02, int02 + 1), false);

                Add(intsIntervalRef0, new IntsInterval(int01 - 1, int01 + 2), false);
                Add(intsIntervalRef0, new IntsInterval(int02 - 1, int02 + 2), false);

                Add(intsIntervalRef0, new IntsInterval(int01 - 1, int02 + 2), false);

                Add(intsIntervalRef0, new IntsInterval(int01, int01), true);
                Add(new IntsInterval(int01, int01), intsIntervalRef0, false);
                Add(intsIntervalRef0, new IntsInterval(int02, int02), true);
                Add(new IntsInterval(int02, int02), intsIntervalRef0, false);

                int int03 = 90;
                Add(new IntsInterval(null, null), intsIntervalRef0, true);
                Add(intsIntervalRef0, new IntsInterval(null, null), false);
                Add(new IntsInterval(null, null), new IntsInterval(int03, null), true);
                Add(new IntsInterval(int03, null), new IntsInterval(null, null), false);
                Add(new IntsInterval(null, int03), new IntsInterval(null, null), false);
                Add(new IntsInterval(null, null), new IntsInterval(null, null), true);

                Add(new IntsInterval(int01, null), intsIntervalRef0, true);
                Add(intsIntervalRef0, new IntsInterval(int01, null), false);
                Add(new IntsInterval(int01, null), new IntsInterval(int03, null), true);
                Add(new IntsInterval(int03, null), new IntsInterval(int01, null), false);
                Add(new IntsInterval(int01, null), new IntsInterval(int03, int03 + 1), true);
                Add(new IntsInterval(int03, int03 + 1), new IntsInterval(int01, null), false);
                Add(new IntsInterval(int01, null), new IntsInterval(int01, int01), true);
                Add(new IntsInterval(int01, int01), new IntsInterval(int01, null), false);
                Add(new IntsInterval(int01, null), new IntsInterval(int01 - 1, int01), false);
                Add(new IntsInterval(int01, null), new IntsInterval(int01 - 2, int01 - 1), false);
                Add(new IntsInterval(int01, null), new IntsInterval(null, int01 - 1), false);
                Add(new IntsInterval(int01, null), new IntsInterval(int01 - 10, null), false);

                Add(new IntsInterval(null, int01), new IntsInterval(int01 - 1, int01), true);
                Add(new IntsInterval(int01 - 1, int01), new IntsInterval(null, int01), false);
                Add(new IntsInterval(null, int01), new IntsInterval(int01 - 1, int01 + 1), false);
                Add(new IntsInterval(null, int01), new IntsInterval(int01 - 2, int01 - 1), true);
                Add(new IntsInterval(int01 - 2, int01 - 1), new IntsInterval(null, int01), false);
                Add(new IntsInterval(null, int01), new IntsInterval(int01, int01 + 1), false);
                Add(new IntsInterval(null, int01), new IntsInterval(int01, int01), true);
                Add(new IntsInterval(int01, int01), new IntsInterval(null, int01), false);
                Add(new IntsInterval(null, int01), new IntsInterval(int01 + 1, int01 + 2), false);
                Add(new IntsInterval(null, int01), new IntsInterval(int01 + 1, null), false);
            }
        }
    }

}


