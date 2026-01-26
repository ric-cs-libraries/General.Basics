using Xunit;
using General.Basics.Bounds.MinAndMax;
using General.Basics.Bounds.Exceptions;

namespace General.Basics.UnitTests.Bounds.MinAndMax;

public class MinAndMaxBytesTests
{
    #region Instanciation
    [Fact]
    public void Constructor_WhenWithoutParam_ShouldSetMinAndMaxToTheirRespectiveMinAndMax()
    {
        // Arrange
        var bounds = new MinAndMaxBytes();

        // Act & Assert
        Assert.Equal(MinAndMaxBytes.MIN_VALUE, bounds.MinValue);
        Assert.Equal(MinAndMaxBytes.MAX_VALUE, bounds.MaxValue);
    }

    [Fact]
    public void Constructor_WhenParamArePassedAndCorrect_ShouldSetMinAndMaxAsSuch()
    {
        // Arrange
        byte min = 10;
        byte max = 20;
        var bounds = new MinAndMaxBytes(min, max);

        // Act & Assert
        Assert.Equal(min, bounds.MinValue);
        Assert.Equal(max, bounds.MaxValue);
    }

    [Fact]
    public void Constructor_WhenMinParamEqualsMaxParam_ShouldSetMinAndMaxAsSuch()
    {
        // Arrange
        byte value = 42;
        var bounds = new MinAndMaxBytes(value, value);

        // Act & Assert
        Assert.Equal(value, bounds.MinValue);
        Assert.Equal(value, bounds.MaxValue);
    }

    [Theory]
    [InlineData(10, 9)]
    [InlineData(1, 0)]
    public void Constructor_WhenMinParamIsGreaterThanMaxParam_ShouldThrowAValueShouldBeLowerOrEqualToExceptionWithTheCorrectMessage
        (byte minValue, byte maxValue)
    {
        // Act & Assert
        var ex = Assert.Throws<ValueShouldBeLowerOrEqualToException<byte>>(() => new MinAndMaxBytes(minValue, maxValue));

        var expectedMessage = string.Format(ValueShouldBeLowerOrEqualToException<byte>.MESSAGE_FORMAT, "minValue", minValue, maxValue);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion Instanciation
}
