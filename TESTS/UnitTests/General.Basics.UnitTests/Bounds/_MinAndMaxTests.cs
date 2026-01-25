using General.Basics.Bounds;
using General.Basics.Bounds.Exceptions;
using System.ComponentModel.DataAnnotations;
using Xunit;


namespace General.Basics.UnitTests.Bounds;

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

    #region IsMinDefined
    [Fact]
    public void IsMinDefined_WhenMinValueIsNull_ShouldReturnFalse()
    {
        // Arrange
        MinAndMax<int> minAndMax = new(minValue: null, maxValue: 100);

        // Act & Assert
        Assert.False(minAndMax.IsMinDefined);
    }

    [Fact]
    public void IsMinDefined_WhenMinValueIsNotNull_ShouldReturnTrue()
    {
        // Arrange
        MinAndMax<int> minAndMax = new(minValue: 200, maxValue: null);

        // Act & Assert
        Assert.True(minAndMax.IsMinDefined);
    }
    #endregion IsMinDefined

    #region IsMaxDefined
    [Fact]
    public void IsMaxDefined_WhenMaxValueIsNull_ShouldReturnFalse()
    {
        // Arrange
        MinAndMax<int> minAndMax = new(minValue: 100, maxValue: null);

        // Act & Assert
        Assert.False(minAndMax.AreMinAndMaxDefined);
    }

    [Fact]
    public void IsMaxDefined_WhenMaxValueIsNotNull_ShouldReturnTrue()
    {
        // Arrange
        MinAndMax<int> minAndMax = new(minValue: null, maxValue: 10);

        // Act & Assert
        Assert.True(minAndMax.IsMaxDefined);
    }
    #endregion IsMaxDefined

    #region AreMinAndMaxDefined
    [Fact]
    public void AreMinAndMaxDefined_WhenMinValueOrMaxValueIsNull_ShouldReturnFalse()
    {
        // Arrange
        MinAndMax<int> minAndMax1 = new(minValue: 100, maxValue: null);
        MinAndMax<int> minAndMax2 = new(minValue: null, maxValue: -5);
        MinAndMax<int> minAndMax3 = new(minValue: null, maxValue: null);

        // Act & Assert
        Assert.False(minAndMax1.AreMinAndMaxDefined);
        Assert.False(minAndMax2.AreMinAndMaxDefined);
        Assert.False(minAndMax3.AreMinAndMaxDefined);
    }

    [Fact]
    public void AreMinAndMaxDefined_WhenMinValueAndMaxValueAreNotNull_ShouldReturnTrue()
    {
        // Arrange
        MinAndMax<int> minAndMax = new(minValue: -5, maxValue: 10);

        // Act & Assert
        Assert.True(minAndMax.AreMinAndMaxDefined);
    }
    #endregion AreMinAndMaxDefined

}


