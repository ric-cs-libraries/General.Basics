using Xunit;

namespace General.Basics.Extensions.UnitTests;

public partial class IEnumerableExtensionTests
{
    #region GetNearestInfValue_
    [Theory]
    [ClassData(typeof(GetNearestInfValueData))]
    public void GetNearestInfValue_WhenNearestInfValueExist_ShouldReturnThisNearestInfValue(int searchedValue, int expectedNearestValue)
    {
        var list = new List<int> { 15, 0, 25, 15, 10, 6, 0, 25, 13, 29, 32, 11 };

        int? nearestInfValue = list.GetNearestInfValue_(searchedValue);
        Assert.Equal(expectedNearestValue, nearestInfValue);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(4)]
    [InlineData(0)]
    [InlineData(-1)]
    public void GetNearestInfValue_WhenNearestInfValueDoesNotExist_ShouldReturnNull(int searchedValue)
    {
        var list = new List<int> { 15, 6, 25 };

        int? nearestInfValue = list.GetNearestInfValue_(searchedValue);

        Assert.Null(nearestInfValue);
    }
    #endregion GetNearestInfValue_


    //===================================================================================
    class GetNearestInfValueData : TheoryData<int, int>
    {
        public GetNearestInfValueData()
        {
            Add(14, 13);
            Add(15, 15);
            Add(29, 29);
            Add(31, 29);
            Add(900, 32);
            Add(24, 15);
            Add(12, 11);
            Add(11, 11);
            Add(9, 6);
            Add(5, 0);
            Add(28, 25);
            Add(32, 32);
            Add(21, 15);
        }
    }
}
