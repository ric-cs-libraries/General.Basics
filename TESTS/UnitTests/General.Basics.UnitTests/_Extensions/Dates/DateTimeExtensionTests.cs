using Xunit;


using General.Basics.Extensions;
using General.Basics._Extensions.Dates;
using General.Basics.Bounds.Intervals;
using General.Basics.Bounds.Exceptions;


namespace General.Basics.Extensions.UnitTests;

public class DateTimeExtensionTests
{
    #region IsBetween(DateTimesInterval)
    [Theory]
    [ClassData(typeof(Fixtures.IsBetween_DateTimesInterval_Data))]
    public void IsBetween_DateTimesInterval__ShouldReturnTheRightValue(DateTime dateTime, DateTimesInterval dateTimesInterval, bool expectedResponse)
    {
        bool result = dateTime.IsInto(dateTimesInterval);

        Assert.Equal(expectedResponse, result);
    }
    #endregion IsBetween(DateTimesInterval)

    #region IsBetween(DateTime?, DateTime?)
    [Theory]
    [ClassData(typeof(Fixtures.IsBetween_DateTimes_Data))]
    public void IsBetween_DateTimes__ShouldReturnTheRightValue(DateTime dateTime, DateTime? lowerDateTime, DateTime? higherDateTime, bool expectedResponse)
    {
        bool result = dateTime.IsBetween(lowerDateTime, higherDateTime);

        Assert.Equal(expectedResponse, result);
    }

    [Theory]
    [ClassData(typeof(Fixtures.IsBetween_InconsistantDateTimesBounds_Data))]
    public void IsBetween_WhenDateTimesBoundsAreInconsistant_ShouldThrowAValueShouldBeLowerOrEqualToException
        (DateTime dateTime, DateTime higherDateTime, DateTime lowerDateTime)
    {
        var ex = Assert.Throws<ValueShouldBeLowerOrEqualToException<DateTime>>(() => dateTime.IsBetween(lowerDateTime:higherDateTime, higherDateTime:lowerDateTime));

        var expectedMessage = string.Format(ValueShouldBeLowerOrEqualToException<DateTime>.MESSAGE_FORMAT, "minValue", higherDateTime, lowerDateTime);
        Assert.Equal(expectedMessage, ex.Message);

    }

    #endregion IsBetween(DateTime?, DateTime?)
}

//=================================================================================
static class Fixtures
{
    internal class IsBetween_DateTimesInterval_Data : TheoryData<DateTime, DateTimesInterval, bool>
    {
        public IsBetween_DateTimesInterval_Data()
        {
            DateTime d0 = DateTime.Parse("26/02/2024");
            DateTime d1 = DateTime.Parse("27/02/2024");
            DateTime d2 = DateTime.Parse("28/02/2024");
            DateTime d3 = DateTime.Parse("29/02/2024");
            DateTime d4 = DateTime.Parse("01/03/2024");

            Add(d1, new DateTimesInterval(d0, d2), true);
            Add(d0, new DateTimesInterval(d0, d2), true);
            Add(d2, new DateTimesInterval(d0, d2), true);

            Add(d1, new DateTimesInterval(d0, d3), true);
            Add(d0, new DateTimesInterval(d0, d3), true);
            Add(d2, new DateTimesInterval(d0, d3), true);
            Add(d3, new DateTimesInterval(d0, d3), true);

            Add(d3, new DateTimesInterval(d2, d4), true);
            Add(d2, new DateTimesInterval(d2, d4), true);
            Add(d4, new DateTimesInterval(d2, d4), true);
            Add(d4, new DateTimesInterval(d4, d4), true);

            Add(d1, new DateTimesInterval(d2, d4), false);
            Add(d4, new DateTimesInterval(d0, d3), false);

            Add(d3, new DateTimesInterval(d2, null), true);
            Add(d3, new DateTimesInterval(null, d4), true);
            Add(d3, new DateTimesInterval(null, d3), true);
            Add(d3, new DateTimesInterval(null, null), true);

            //
            DateTime dt0 = DateTime.Parse("26/02/2024 00:00:00");
            DateTime dt1 = DateTime.Parse("26/02/2024 00:00:01");
            DateTime dt2 = DateTime.Parse("26/02/2024 00:00:02");
            DateTime dt3 = DateTime.Parse("26/02/2024 00:00:03");
            DateTime dt4 = DateTime.Parse("26/02/2024 00:00:04");

            Add(dt1, new DateTimesInterval(dt0, dt2), true);
            Add(dt0, new DateTimesInterval(dt0, dt2), true);
            Add(dt2, new DateTimesInterval(dt0, dt2), true);

            Add(dt1, new DateTimesInterval(dt0, dt3), true);
            Add(dt0, new DateTimesInterval(dt0, dt3), true);
            Add(dt2, new DateTimesInterval(dt0, dt3), true);
            Add(dt3, new DateTimesInterval(dt0, dt3), true);
            Add(dt3, new DateTimesInterval(dt2, dt4), true);
            Add(dt3, new DateTimesInterval(dt3, dt3), true);
            Add(dt3, new DateTimesInterval(dt3, dt4), true);
            Add(dt3, new DateTimesInterval(dt4, dt4), false);

            Add(dt3, new DateTimesInterval(dt2, dt4), true);
            Add(dt2, new DateTimesInterval(dt2, dt4), true);
            Add(dt4, new DateTimesInterval(dt2, dt4), true);
            Add(dt4, new DateTimesInterval(dt4, dt4), true);

            Add(dt1, new DateTimesInterval(dt2, dt4), false);
            Add(dt4, new DateTimesInterval(dt0, dt3), false);

            //
            Add(d0, new DateTimesInterval(dt0, d0), true);
            Add(dt0, new DateTimesInterval(dt0, d0), true);

            //
            Add(dt0, new DateTimesInterval(null, d0), true);
            Add(dt0, new DateTimesInterval(d0, null), true);
        }
    }

    internal class IsBetween_DateTimes_Data : TheoryData<DateTime, DateTime?, DateTime?, bool>
    {
        public IsBetween_DateTimes_Data()
        {
            DateTime d0 = DateTime.Parse("26/02/2024");
            DateTime d1 = DateTime.Parse("27/02/2024");
            DateTime d2 = DateTime.Parse("28/02/2024");
            DateTime d3 = DateTime.Parse("29/02/2024");
            DateTime d4 = DateTime.Parse("01/03/2024");

            Add(d1, d0, d2, true);
            Add(d0, d0, d2, true);
            Add(d2, d0, d2, true);

            Add(d1, d0, d3, true);
            Add(d0, d0, d3, true);
            Add(d2, d0, d3, true);
            Add(d3, d0, d3, true);

            Add(d3, d2, d4, true);
            Add(d2, d2, d4, true);
            Add(d4, d2, d4, true);
            Add(d4, d4, d4, true);

            Add(d1, d2, d4, false);
            Add(d4, d0, d3, false);

            Add(d3, d2, null, true);
            Add(d3, null, d4, true);
            Add(d3, null, d3, true);
            Add(d3, null, null, true);

            //
            DateTime dt0 = DateTime.Parse("26/02/2024 00:00:00");
            DateTime dt1 = DateTime.Parse("26/02/2024 00:00:01");
            DateTime dt2 = DateTime.Parse("26/02/2024 00:00:02");
            DateTime dt3 = DateTime.Parse("26/02/2024 00:00:03");
            DateTime dt4 = DateTime.Parse("26/02/2024 00:00:04");

            Add(dt1, dt0, dt2, true);
            Add(dt0, dt0, dt2, true);
            Add(dt2, dt0, dt2, true);

            Add(dt1, dt0, dt3, true);
            Add(dt0, dt0, dt3, true);
            Add(dt2, dt0, dt3, true);
            Add(dt3, dt0, dt3, true);
            Add(dt3, dt2, dt4, true);
            Add(dt3, dt3, dt3, true);
            Add(dt3, dt3, dt4, true);
            Add(dt3, dt4, dt4, false);

            Add(dt3, dt2, dt4, true);
            Add(dt2, dt2, dt4, true);
            Add(dt4, dt2, dt4, true);
            Add(dt4, dt4, dt4, true);

            Add(dt1, dt2, dt4, false);
            Add(dt4, dt0, dt3, false);

            //
            Add(d0, dt0, d0, true);
            Add(dt0, dt0, d0, true);

            //
            Add(dt0, null, d0, true);
            Add(dt0, d0, null, true);
        }
    }

    internal class IsBetween_InconsistantDateTimesBounds_Data : TheoryData<DateTime, DateTime, DateTime>
    {
        public IsBetween_InconsistantDateTimesBounds_Data()
        {
            DateTime d0 = DateTime.Parse("26/02/2024");
            DateTime d1 = DateTime.Parse("28/02/2024");
            DateTime d2 = DateTime.Parse("29/02/2024");

            Add(d0, d1, d0);
            Add(d0, d2, d0);
            Add(d0, d2, d1);

            //
            DateTime dt0 = DateTime.Parse("26/02/2024 00:00:00");
            DateTime dt1 = DateTime.Parse("26/02/2024 00:00:02");
            DateTime dt2 = DateTime.Parse("26/02/2024 00:00:03");

            Add(dt0, dt1, dt0);
            Add(dt0, dt2, dt0);
            Add(dt0, dt2, dt1);
        }
    }
}