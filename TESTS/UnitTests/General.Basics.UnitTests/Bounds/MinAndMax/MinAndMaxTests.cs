using General.Basics.Bounds.Exceptions;
using General.Basics.Bounds.MinAndMax;
using System.ComponentModel.DataAnnotations;
using Xunit;


namespace General.Basics.UnitTests.Bounds.MinAndMax;

public class MinAndMaxTests
{
    #region Instanciation
    [Theory]
    [InlineData(-10, 31)]
    [InlineData(-1, -1)]
    [InlineData(0, 0)]
    [InlineData(100, 100)]
    [InlineData(100, 101)]
    public void Instanciation_WhenMinValueIsLowerOrEqualToMaxValue_ShouldInitializeCorrectlyTheProperties(int minValue, int maxValue)
    {
        // Act
        MinAndMax<int> minAndMax = new(minValue, maxValue);

        // Assert
        Assert.Equal(minAndMax.MinValue, minValue);
        Assert.Equal(minAndMax.MaxValue, maxValue);
    }

    [Theory]
    [InlineData(10, 9)]
    [InlineData(0, -1)]
    [InlineData(-1, -2)]
    public void Instanciation_WhenMinValueIsGreaterThanMaxValue_ShouldThrowAValueShouldNotBeGreaterThanException(int minValue, int maxValue)
    {
        // Act & Assert
        var ex = Assert.Throws<ValueShouldBeLowerOrEqualToException<int>>(() => new MinAndMax<int>(minValue, maxValue));

        var expectedMessage = string.Format(ValueShouldBeLowerOrEqualToException<int>.MESSAGE_FORMAT, "minValue", minValue, maxValue);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion Instanciation

}


