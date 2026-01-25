using General.Basics.ErrorHandling;
using General.Basics.Extensions;
using Xunit;


namespace General.Basics.UnitTests.Extensions;

public class IComparableExtensionTests
{
    #region IsGreaterThan
    [Theory]
    [InlineData(10, 9)]
    [InlineData(-10, -11)]
    [InlineData(0, -1)]
    public void IsGreaterThan_WhenLeftValueIsGreater_ShouldReturnTrue(int leftValue, int rightValue)
    {
        //
        bool result = leftValue.IsGreaterThan(rightValue);

        //
        Assert.True(result);
    }

    [Theory]
    [InlineData(9, 10)]
    [InlineData(-11, -10)]
    [InlineData(-1, 0)]
    [InlineData(0, 0)]
    [InlineData(10, 10)]
    [InlineData(-2, -2)]
    public void IsGreaterThan_WhenLeftValueIsLowerOrEqual_ShouldReturnFalse(int leftValue, int rightValue)
    {
        //
        bool result = leftValue.IsGreaterThan(rightValue);

        //
        Assert.False(result);
    }

    [Theory]
    [ClassData(typeof(IsGreaterThan_WhenLeftValueIsGreater_DateTime_Data))]
    public void IsGreaterThan_WhenLeftValueIsGreater_ShouldReturnTrue_(DateTime leftValue, DateTime rightValue)
    {
        //
        bool result = leftValue.IsGreaterThan(rightValue);

        //
        Assert.True(result);
    }

    [Theory]
    [ClassData(typeof(IsGreaterThan_WhenLeftValueIsLowerOrEqual_DateTime_Data))]
    public void IsGreaterThan_WhenLeftValueIsLowerOrEqual_ShouldReturnFalse_(DateTime leftValue, DateTime rightValue)
    {
        //
        bool result = leftValue.IsGreaterThan(rightValue);

        //
        Assert.False(result);
    }

    class IsGreaterThan_WhenLeftValueIsGreater_DateTime_Data : TheoryData<DateTime, DateTime>
    {
        public IsGreaterThan_WhenLeftValueIsGreater_DateTime_Data()
        {
            DateTime d1 = DateTime.Now;

            Add(d1, d1.AddSeconds(-1));
        }
    }

    class IsGreaterThan_WhenLeftValueIsLowerOrEqual_DateTime_Data : TheoryData<DateTime, DateTime>
    {
        public IsGreaterThan_WhenLeftValueIsLowerOrEqual_DateTime_Data()
        {
            DateTime d1 = DateTime.Now;

            Add(d1, d1.AddSeconds(1));
            Add(d1, d1.AddSeconds(0));
            Add(d1, d1);
        }
    }
    #endregion IsGreaterThan

    #region IsLowerThan
    [Theory]
    [InlineData(9, 10)]
    [InlineData(-11, -10)]
    [InlineData(-1, 0)]
    public void IsLowerThan_WhenLeftValueIsLower_ShouldReturnTrue(int leftValue, int rightValue)
    {
        //
        bool result = leftValue.IsLowerThan(rightValue);

        //
        Assert.True(result);
    }

    [Theory]
    [InlineData(10, 9)]
    [InlineData(-10, -11)]
    [InlineData(0, -1)]
    [InlineData(0, 0)]
    [InlineData(10, 10)]
    [InlineData(-2, -2)]
    public void IsLowerThan_WhenLeftValueIsGreaterOrEqual_ShouldReturnFalse(int leftValue, int rightValue)
    {
        //
        bool result = leftValue.IsLowerThan(rightValue);

        //
        Assert.False(result);
    }

    [Theory]
    [ClassData(typeof(IsLowerThan_WhenLeftValueIsLower_DateTime_Data))]
    public void IsLowerThan_WhenLeftValueIsLower_ShouldReturnTrue_(DateTime leftValue, DateTime rightValue)
    {
        //
        bool result = leftValue.IsLowerThan(rightValue);

        //
        Assert.True(result);
    }

    [Theory]
    [ClassData(typeof(IsLowerThan_WhenLeftValueIsGreaterOrEqual_DateTime_Data))]
    public void IsLowerThan_WhenLeftValueIsLowerOrEqual_ShouldReturnFalse_(DateTime leftValue, DateTime rightValue)
    {
        //
        bool result = leftValue.IsLowerThan(rightValue);

        //
        Assert.False(result);
    }

    class IsLowerThan_WhenLeftValueIsLower_DateTime_Data : TheoryData<DateTime, DateTime>
    {
        public IsLowerThan_WhenLeftValueIsLower_DateTime_Data()
        {
            DateTime d1 = DateTime.Now;

            Add(d1, d1.AddSeconds(1));
        }
    }

    class IsLowerThan_WhenLeftValueIsGreaterOrEqual_DateTime_Data : TheoryData<DateTime, DateTime>
    {
        public IsLowerThan_WhenLeftValueIsGreaterOrEqual_DateTime_Data()
        {
            DateTime d1 = DateTime.Now;

            Add(d1, d1.AddSeconds(-1));
            Add(d1, d1.AddSeconds(0));
            Add(d1, d1);
        }
    }
    #endregion IsLowerThan

    #region IsGreaterOrEqualTo
    [Theory]
    [InlineData(10, 9)]
    [InlineData(-10, -11)]
    [InlineData(0, -1)]
    [InlineData(9, 9)]
    [InlineData(-11, -11)]
    [InlineData(0, 0)]
    public void IsGreaterOrEqualTo_WhenLeftValueIsGreaterOrEqual_ShouldReturnTrue(int leftValue, int rightValue)
    {
        //
        bool result = leftValue.IsGreaterOrEqualTo(rightValue);

        //
        Assert.True(result);
    }

    [Theory]
    [InlineData(9, 10)]
    [InlineData(-11, -10)]
    [InlineData(-1, 0)]
    public void IsGreaterOrEqualTo_WhenLeftValueIsLower_ShouldReturnFalse(int leftValue, int rightValue)
    {
        //
        bool result = leftValue.IsGreaterOrEqualTo(rightValue);

        //
        Assert.False(result);
    }
    #endregion IsGreaterOrEqualTo

    #region IsLowerOrEqualTo
    [Theory]
    [InlineData(9, 10)]
    [InlineData(-11, -10)]
    [InlineData(-1, 0)]
    [InlineData(9, 9)]
    [InlineData(-11, -11)]
    [InlineData(0, 0)]
    public void IsLowerOrEqualTo_WhenLeftValueIsLowerOrEqual_ShouldReturnTrue(int leftValue, int rightValue)
    {
        //
        bool result = leftValue.IsLowerOrEqualTo(rightValue);

        //
        Assert.True(result);
    }

    [Theory]
    [InlineData(10, 9)]
    [InlineData(-10, -11)]
    [InlineData(0, -1)]
    public void IsLowerOrEqualTo_WhenLeftValueIsGreater_ShouldReturnFalse(int leftValue, int rightValue)
    {
        //
        bool result = leftValue.IsLowerOrEqualTo(rightValue);

        //
        Assert.False(result);
    }
    #endregion IsLowerOrEqualTo

    #region IsEqualTo
    [Theory]
    [InlineData(9, 9)]
    [InlineData(-11, -11)]
    [InlineData(0, 0)]
    public void IsEqualTo_WhenLeftValueIsEqual_ShouldReturnTrue(int leftValue, int rightValue)
    {
        //
        bool result = leftValue.IsEqualTo(rightValue);

        //
        Assert.True(result);
    }

    [Theory]
    [InlineData(10, 9)]
    [InlineData(-10, -11)]
    [InlineData(0, -1)]
    [InlineData(-1, 0)]
    public void IsEqualTo_WhenLeftValueIsNotEqual_ShouldReturnFalse(int leftValue, int rightValue)
    {
        //
        bool result = leftValue.IsEqualTo(rightValue);

        //
        Assert.False(result);
    }

    [Theory]
    [ClassData(typeof(IsEqualTo_WhenLeftValueIsEqual_DateTime_Data))]
    public void IsEqualTo_WhenLeftValueIsEqual_ShouldReturnTrue_(DateTime leftValue, DateTime rightValue)
    {
        //
        bool result = leftValue.IsEqualTo(rightValue);

        //
        Assert.True(result);
    }

    [Theory]
    [ClassData(typeof(IsEqualTo_WhenLeftValueIsNotEqual_DateTime_Data))]
    public void IsEqualTo_WhenLeftValueIsNotEqual_ShouldReturnFalse_(DateTime leftValue, DateTime rightValue)
    {
        //
        bool result = leftValue.IsEqualTo(rightValue);

        //
        Assert.False(result);
    }

    class IsEqualTo_WhenLeftValueIsEqual_DateTime_Data : TheoryData<DateTime, DateTime>
    {
        public IsEqualTo_WhenLeftValueIsEqual_DateTime_Data()
        {
            DateTime d1 = DateTime.Now;

            Add(d1, d1.AddSeconds(0));
            Add(d1, d1);
        }
    }

    class IsEqualTo_WhenLeftValueIsNotEqual_DateTime_Data : TheoryData<DateTime, DateTime>
    {
        public IsEqualTo_WhenLeftValueIsNotEqual_DateTime_Data()
        {
            DateTime d1 = DateTime.Now;

            Add(d1, d1.AddSeconds(-1));
            Add(d1, d1.AddSeconds(1));
            Add(d1.AddSeconds(-1), d1);
            Add(d1.AddSeconds(1), d1);
        }
    }
    #endregion IsEqualTo

}
