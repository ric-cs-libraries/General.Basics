using Xunit;


using General.Basics.Others;
using General.Basics.Others.Exceptions.UnitTests;
using General.Basics.Others.Exceptions;

namespace General.Basics.Others.UnitTests;

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
        var ex = Assert.Throws<ValueShouldBeLowerOrEqualToException<int>>(() => new IntsInterval(minValue, maxValue));

        var expectedMessage = string.Format(ValueShouldBeLowerOrEqualToException<int>.MESSAGE_FORMAT, "Interval minValue", minValue, maxValue);
        Assert.Equal(expectedMessage, ex.Message);
    }

    #region Contains
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
    #endregion Contains
}
